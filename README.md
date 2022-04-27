# Textractor Forms

Download release: https://drive.google.com/file/d/1DxBFjC1K7RhLAYG3GXEG2y0SOzIV5GVu/view?usp=sharing

Textractor Forms is a fork of Textractor (https://github.com/Artikash/Textractor) that simplifies the interface and integrates a modified version of Sugoi Translator (https://github.com/leminhyen2/Sugoi-Japanese-Translator). NOTE: 64-BIT IS NOT SUPPORTED YET.

## Usage

- Run the exe file.
- Run the program you want to hook into.
- Press the "Attach to game" button and select your program.
- Click through some text so that the texthook can discover hooks.
- Find the correct hook from the dropdown at the top.
- Optional: Press the "Zen mode" button to activate a minimalistic interface.

## Motivation

This project originally began with the purpose of porting the GUI from QT to C# WinForms hence the name. Ultimately, this project was designed for my personal use so it may not be perfect for everyone.

# Building

Open the .sln in Visual Studio 2022 and build with C++20. For now, only 32-bit is supported. The translation functionality will be missing because the translator is too large to upload. The translator along with the rest of the binaries can be downloaded from https://drive.google.com/file/d/1DxBFjC1K7RhLAYG3GXEG2y0SOzIV5GVu/view?usp=sharing.

## Project Structure

### Extractor

Builds the textractor engine into a dll and exposes a C interface.

### Socket

Builds the tcp network code into a dll and exposes a C interface.
The network code could be done directly in C# but I have less experience
in that area.

### Translator

Turns Sugoi Translator into a translation server over TCP.
Side note: It is actually a client not a server and due to this,
the translator can be moved to another computer
if the ip address is changed in the python code.
You might want to do this since the translator requires
a fairly fast computer.

### Textractor Forms (GUI)

This is the main GUI application which brings everything together.
It uses PInvoke to call native functions
from extractor.dll and socket.dll and uses TCP to communicate with the
translation server written in python.
