@if exist %1.s set infile=%1.s
@if exist %1.c set infile=%1.c
arm-none-eabi-gcc.exe -c %infile% -o %1.o %2 -Itools\ -nostdlib -fno-builtin -nostartfiles -nodefaultlibs -mcpu=arm7tdmi

arm-none-eabi-ld -nostdlib -T tools\linker.ld -N -e _start -o %1.exe %1.o tools\libc.a tools\libsys.a
