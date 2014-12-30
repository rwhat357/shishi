@echo off

rem All programs are single-source file here
rem ----------------------------------------

rem Build all assembly test programs
for %%f in (src\*.s) do call tools\driver.cmd "%%~pnf" && move "%%~pnf.exe" .

rem Build all C test programs
for %%f in (src\*.c) do call tools\driver.cmd "%%~pnf" && move "%%~pnf.exe" .
