#include "host.h"
#include "hookcode.h"
#include "module.h"
#include <io.h>
#include <fcntl.h>

struct InfoForExtension {
	const char* name;
	int64_t value;
};

using ExtensionCallback = wchar_t* (*)(wchar_t*, const InfoForExtension*);

static std::vector<ExtensionCallback> extensions;

//template <class T, class... Args>
//void Print(T&& obj, Args&&... args) {
//	auto& out = std::wcout;
//	out << obj << "\t" << std::flush;
//	if constexpr (sizeof...(args) == 0) {
//		out << std::endl;
//	} else {
//		Print(args...);
//	}
//}

std::unordered_multimap<std::wstring, DWORD> GetFilteredProcesses() {
	std::unordered_multimap<std::wstring, DWORD> processes;
	for (auto& [processId, processName] : GetAllProcesses())
		if (processName.has_value() && !processes.contains(processName.value()))
			processes.insert({ processName.value(), processId });
	return processes;
}

struct Callbacks {
	void(*cb1)(DWORD);
	void(*cb2)(DWORD);
	void(*cb3)(void*, const wchar_t*);
	void(*cb4)(void*, const wchar_t*);
	void(*cb5)(void*, const wchar_t*, const wchar_t*);
} cbs{};

void ProcessConnected(DWORD pid) {
	//Print(L"Native ProcessConnected", pid);
	cbs.cb1(pid);
}

void ProcessDisconnected(DWORD pid) {
	//Print(L"Native ProcessDisconnected", pid);
	cbs.cb2(pid);
}

void ThreadAdded(TextThread& thread) {
	//Print(L"Native ThreadAdded", thread.name);
	cbs.cb3(&thread, thread.name.c_str());
}

void ThreadRemoved(TextThread& thread) {
	//Print(L"Native ThreadRemoved", thread.name);
	cbs.cb4(&thread, thread.name.c_str());
}

static std::mutex extenMtx;
bool DispatchSentenceToExtensions(std::wstring& sentence, const InfoForExtension* sentenceInfo) {
	wchar_t* sentenceBuffer = (wchar_t*)HeapAlloc(GetProcessHeap(), HEAP_GENERATE_EXCEPTIONS, (sentence.size() + 1) * sizeof(wchar_t));
	wcscpy_s(sentenceBuffer, sentence.size() + 1, sentence.c_str());
	std::lock_guard<std::mutex> lock(extenMtx);
	for (const auto& extension : extensions)
		if (!*(sentenceBuffer = extension(sentenceBuffer, sentenceInfo))) break;
	sentence = sentenceBuffer;
	HeapFree(GetProcessHeap(), 0, sentenceBuffer);
	return !sentence.empty();
}

static std::atomic<TextThread*> current = nullptr;

std::array<InfoForExtension, 20> GetSentenceInfo(TextThread& thread) {
	void (*AddText)(int64_t, const wchar_t*) = [](int64_t number, const wchar_t* text) {
		//QMetaObject::invokeMethod(This, [number, text = std::wstring(text)]{ if (TextThread* thread = Host::GetThread(number)) thread->Push(text.c_str()); });
	};
	void (*AddSentence)(int64_t, const wchar_t*) = [](int64_t number, const wchar_t* sentence) {
		// pointer from Host::GetThread may not stay valid unless on main thread
		//QMetaObject::invokeMethod(This, [number, sentence = std::wstring(sentence)]{ if (TextThread* thread = Host::GetThread(number)) thread->AddSentence(sentence); });
	};
	DWORD(*GetSelectedProcessId)() = [] {
		return DWORD();//selectedProcessId.load();
	};

	return
	{ {
	{ "current select", &thread == current },
	{ "text number", thread.handle },
	{ "process id", thread.tp.processId },
	{ "hook address", (int64_t)thread.tp.addr },
	{ "text handle", thread.handle },
	{ "text name", (int64_t)thread.name.c_str() },
	{ "add sentence", (int64_t)AddSentence },
	{ "add text", (int64_t)AddText },
	{ "get selected process id", (int64_t)GetSelectedProcessId },
	{ "void (*AddSentence)(int64_t number, const wchar_t* sentence)", (int64_t)AddSentence },
	{ "void (*AddText)(int64_t number, const wchar_t* text)", (int64_t)AddText },
	{ "DWORD (*GetSelectedProcessId)()", (int64_t)GetSelectedProcessId },
	{ nullptr, 0 } // nullptr marks end of info array
	} };
}

bool SentenceReceived(TextThread& thread, std::wstring& text) {
	//Print(L"Native SentenceReceived", thread.name, text);

	if (!DispatchSentenceToExtensions(text, GetSentenceInfo(thread).data()))
		return false;

	cbs.cb5(&thread, thread.name.c_str(), text.c_str());
	return true;
}

void Init() {
	TextThread::flushDelay = 0;
	//(void)_setmode(_fileno(stdout), _O_U16TEXT);
	//
	//CONSOLE_FONT_INFOEX cfi{};
	//cfi.cbSize = sizeof cfi;
	//cfi.nFont = 0;
	//cfi.dwFontSize.X = 0;
	//cfi.dwFontSize.Y = 20;
	//cfi.FontFamily = FF_DONTCARE;
	//cfi.FontWeight = FW_NORMAL;
	//wcscpy(cfi.FaceName, L"MS Gothic");
	//SetCurrentConsoleFontEx(GetStdHandle(STD_OUTPUT_HANDLE), FALSE, &cfi);
}

#define EXT_DLL_EXPORT __declspec(dllexport)

extern "C" {

	EXT_DLL_EXPORT void SetCurrentTextThread(TextThread* p) {
		current = p;
	}

	EXT_DLL_EXPORT void LoadExtension(const wchar_t* dll) {
		if (HMODULE module = LoadLibraryW(dll)) {
			if (auto callback = (ExtensionCallback)GetProcAddress(module, "OnNewSentence")) {
				extensions.push_back(callback);
				return;
			}
			FreeLibrary(module);
		}
	}

	EXT_DLL_EXPORT const wchar_t* Ext_GetProcessList() {
		static std::wstring processList;
		processList.clear();
		for (auto& [processId, processName] : GetAllProcesses())
			if (processName.has_value())
				processList += std::to_wstring(processId) + L"|" + processName.value() + L"|";
		return processList.c_str();
	}

	EXT_DLL_EXPORT void Ext_Start(
		void(*cb1)(DWORD),
		void(*cb2)(DWORD),
		void(*cb3)(void*, const wchar_t*),
		void(*cb4)(void*, const wchar_t*),
		void(*cb5)(void*, const wchar_t*, const wchar_t*)
	)
	{
		cbs = { cb1, cb2, cb3, cb4, cb5 };
		Host::Start(ProcessConnected, ProcessDisconnected, ThreadAdded, ThreadRemoved, SentenceReceived);
	}

	EXT_DLL_EXPORT void Ext_InjectProcess(int pid) {
		for (auto& [processId, processName] : GetAllProcesses())
			if (processId == pid)
				Host::InjectProcess((DWORD)pid);
	}

	EXT_DLL_EXPORT void Ext_DetachProcess(int pid) {
		for (auto& [processId, processName] : GetAllProcesses())
			if (processId == pid)
				Host::DetachProcess((DWORD)pid);
	}
}
