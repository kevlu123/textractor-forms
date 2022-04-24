#include "host.h"
#include "hookcode.h"
#include "module.h"
#include <fstream>
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

std::wstring ExtractFilenameFromPath(std::wstring path) {
	auto pos = path.find_last_of('\\');
	if (pos == std::wstring::npos)
		return path;
	return path.substr(pos + 1);
}

void ProcessConnected(DWORD pid) {
	Print(L"ProcessConnected", pid);
}

void ProcessDisconnected(DWORD pid) {
	Print(L"ProcessDisconnected", pid);
}

void ThreadAdded(TextThread& thread) {
	Print(L"ThreadAdded", thread.name);
}

void ThreadRemoved(TextThread& thread) {
	Print(L"ThreadRemoved", thread.name);
}

bool Contains(const std::wstring& s, const std::wstring& x) {
	return s.find(x) != std::wstring::npos;
}

bool SentenceReceived(TextThread& thread, std::wstring& text) {
	//if (thread.tp.addr != 1977016304) return false;
	//if (thread.tp.ctx != 10545442) return false;

	Print(L"SentenceReceived", thread.name, text);
	return true;
}

int main() {

	(void)_setmode(_fileno(stdout), _O_U16TEXT);
	//_setmode(_fileno(stdin), _O_U16TEXT);

	CONSOLE_FONT_INFOEX cfi{};
	cfi.cbSize = sizeof cfi;
	cfi.nFont = 0;
	cfi.dwFontSize.X = 0;
	cfi.dwFontSize.Y = 20;
	cfi.FontFamily = FF_DONTCARE;
	cfi.FontWeight = FW_NORMAL;
	wcscpy(cfi.FaceName, L"MS Gothic");
	SetCurrentConsoleFontEx(GetStdHandle(STD_OUTPUT_HANDLE), FALSE, &cfi);

	auto processes = GetFilteredProcesses();
	//for (auto& [processId, processName] : processes) {
	//	out << processId << "\t" << ExtractFilenameFromPath(processName) << std::endl;
	//}

	std::wstring name = LR"(C:\Stuff\Visual Novels\Sugar Style FD\SugarStyle_恋人以上夫婦未満アフターストーリー\SugarStyle_恋人以上夫婦未満アフターストーリー.exe)";

	if (!processes.contains(name)) {
		Print(L"No Process selected");
		return 0;
	} else {
		Print(L"Selected process: ", name);
	}

	Host::Start(ProcessConnected, ProcessDisconnected, ThreadAdded, ThreadRemoved, SentenceReceived);

	auto range = processes.equal_range(name);
	DWORD pid = 0;
	for (auto& p = range.first; p != range.second; ++p) {
		Host::InjectProcess(p->second);
		pid = p->second;
	}

	while (true)
	{
		Sleep(100);
	}

	return 0;
}
