@echo off

rem All programs are single-source file here
rem ----------------------------------------

rem Build all C test programs with INPUT by default
for %%f in (src\*.c) do call tools\driver.cmd "%%~pnf" -DINPUT && move "%%~pnf.exe" .

rem Build all C test programs without INPUT as an option
for %%f in (src\*.c) do call tools\driver.cmd "%%~pnf" && move "%%~pnf.exe" "%%~nf_no_input.exe"
