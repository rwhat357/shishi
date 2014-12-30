/* ATOS (ARMSim Toy OS) 2.0  [Level: C]
 * (c) 2014, BJU
 *--------------------------------------------------
 * Definitions for user-mode library (a.k.a. "libc")
 *------------------------------------------------*/
#ifndef ATOS_LIBC_H
#define ATOS_LIBC_H

/* Need NULL */
#define NULL ((void *)0)

/* Avoid signed-byte loads by using unsigned chars */
typedef unsigned char uchar;

/* Everybody needs a size_t... */
typedef unsigned int size_t;

/* Write character to console */
void putc(uchar);

/* Write NUL-terminated string to console */
void puts(const uchar *);

/* Read character from console */
char getc();

/* Read '\r'-terminated string from console; NUL-terminate the data */
size_t gets(uchar *, size_t );

/* Convert NUL-terminated string to signed integer (very naively) */
int atoi(const uchar *);

/* Convert integer to NUL-terminated string */
uchar *itoa(int, uchar *, size_t );

/* Read integer (string) from console; convenience... */
int geti();

/* Write integer (string) to console; convenience... */
void puti(int);

#endif