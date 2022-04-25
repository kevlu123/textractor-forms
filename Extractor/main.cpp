#include "host.h"
#include "hookcode.h"
#include "module.h"
#include <io.h>
#include <fcntl.h>

template <class T, class... Args>
void Print(T&& obj, Args&&... args) {
	auto& out = std::wcout;
	out << obj << "\t" << std::flush;
	if constexpr (sizeof...(args) == 0) {
		out << std::endl;
	} else {
		Print(args...);
	}
}

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
	Print(L"Native ProcessConnected", pid);
	cbs.cb1(pid);
}

void ProcessDisconnected(DWORD pid) {
	Print(L"Native ProcessDisconnected", pid);
	cbs.cb2(pid);
}

void ThreadAdded(TextThread& thread) {
	Print(L"Native ThreadAdded", thread.name);
	cbs.cb3(&thread, thread.name.c_str());
}

void ThreadRemoved(TextThread& thread) {
	Print(L"Native ThreadRemoved", thread.name);
	cbs.cb4(&thread, thread.name.c_str());
}

bool SentenceReceived(TextThread& thread, std::wstring& text) {
	Print(L"Native SentenceReceived", thread.name, text);
	cbs.cb5(&thread, thread.name.c_str(), text.c_str());
	return true;
}

void Init() {
	(void)_setmode(_fileno(stdout), _O_U16TEXT);

	CONSOLE_FONT_INFOEX cfi{};
	cfi.cbSize = sizeof cfi;
	cfi.nFont = 0;
	cfi.dwFontSize.X = 0;
	cfi.dwFontSize.Y = 20;
	cfi.FontFamily = FF_DONTCARE;
	cfi.FontWeight = FW_NORMAL;
	wcscpy(cfi.FaceName, L"MS Gothic");
	SetCurrentConsoleFontEx(GetStdHandle(STD_OUTPUT_HANDLE), FALSE, &cfi);
}

#define EXT_DLL_EXPORT __declspec(dllexport)

extern "C" {
	EXT_DLL_EXPORT const wchar_t* Ext_GetProcessList() {
		static std::wstring processList;
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
}
