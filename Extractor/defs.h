#pragma once

// texthook/defs.h
// 8/23/2013 jichi

// Pipes


#define X(x) x// L"X"

constexpr auto HOOK_PIPE = X(L"\\\\.\\pipe\\TEXTRACTOR_HOOK");
constexpr auto HOST_PIPE = X(L"\\\\.\\pipe\\TEXTRACTOR_HOST");

// Sections

constexpr auto ITH_SECTION_ = L"VNR_SECTION_"; // _%d

// Mutexes

constexpr auto ITH_HOOKMAN_MUTEX_ = X(L"VNR_HOOKMAN_"); // ITH_HOOKMAN_%d
constexpr auto CONNECTING_MUTEX = X(L"TEXTRACTOR_CONNECTING_PIPES");

// Events

constexpr auto PIPE_AVAILABLE_EVENT = X(L"TEXTRACTOR_PIPE_AVAILABLE");

// Files

constexpr auto ITH_DLL = L"texthook"; // .dll but LoadLibrary automatically adds that
constexpr auto& GAME_CONFIG_FILE = L"TextractorConfig.txt";

#undef X
// EOF
