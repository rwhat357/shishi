PHDRS {
    kernel PT_LOAD AT(0x0000) ;
    program PT_LOAD AT(0x1000) ;
}

SECTIONS
{
    .kernel 0x0000 : {
        tools\libsys.a:boot.o
        tools\libsys.a:kernel.o
    } :kernel


    . = 0x1000;
    .text 0x1000 :  {
        tools\libc.a:crt.o
        tools\libc.a:libc.o
        *.o
    } :program

}

EXTERN(_reset)
ENTRY(_start)
