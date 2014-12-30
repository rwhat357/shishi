using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;


namespace armsim
{
    public static class TestDecodeExecuteSimII
    {
        public static void runTests()
        {
            Console.WriteLine("Testing TestDecodeExecuteSimII class methods");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("Testing start_core_cmp_exe_tests()");
            start_core_cmp_exe_tests();

            Console.WriteLine("Testing start_core_locals_exe_tests()");
            start_core_locals_exe_tests();

            Console.WriteLine("Testing start_core_branch_exe_tests()");
            start_core_branch_exe_tests();

            Console.WriteLine("Testing start_core_pointers_exe_tests()");
            start_core_pointers_exe_tests();

            return;

        }

        private static void start_core_branch_exe_tests()
        {
            Memory mem = new Memory(); // 32 KB
            Registers regs = new Registers();
            CPU cpu = new CPU(mem, regs);
            string curInstStr = ""; // facilitates debugging
            List<string> disLst = new List<string>(); // instruction disassembled list
            regs.updateProgramCounter(0x1000);   // set PC to 0x1000
            regs.updateStackPointer(0x00007000); // set SP to 0x00007000

            /******************************* load instruction set 1 to memory ********************************/
            //                    INSTRUCTION         ADDRESS           DISASSEMBLED
            uint[] instLst = {    0xe3a05000,    //  1000:	e3a05000 	mov	r5, #0
                                  0xe3a00000,    //  1004:	e3a00000 	mov	r0, #0
                                  0xe3a01001,    //  1008:	e3a01001 	mov	r1, #1
                                  0xea000000,    //  100c:	ea000000 	b	1014 <label1>
                                  0xe2855001,    //  1010:	e2855001 	add	r5, r5, #1
                                  0xe1500000,    //  1014:	e1500000 	cmp	r0, r0
                                  0x0a000000,    //  1018:	0a000000 	beq	1020 <label2>
                                  0xe2855001,    //  101c:	e2855001 	add	r5, r5, #1
                                  0xe1500001,    //  1020:	e1500001 	cmp	r0, r1
                                  0x0a000000,    //  1024:	0a000000 	beq	102c <label3>
                                  0xe2855001,    //  1028:	e2855001 	add	r5, r5, #1
                                  0xe1500000,    //  102c:	e1500000 	cmp	r0, r0
                                  0x1a000000,    //  1030:	1a000000 	bne	1038 <label4>
                                  0xe2855001,    //  1034:	e2855001 	add	r5, r5, #1
                                  0xe1500001,    //  1038:	e1500001 	cmp	r0, r1
                                  0x1a000000,    //  103c:	1a000000 	bne	1044 <label5>
                                  0xe2855001,    //  1040:	e2855001 	add	r5, r5, #1
                                  0xe3a00005,    //  1044:	e3a00005 	mov	r0, #5
                                  0xe3500003,    //  1048:	e3500003 	cmp	r0, #3
                                  0x2a000000,    //  104c:	2a000000 	bcs	1054 <label6>
                                  0xe2855001,    //  1050:	e2855001 	add	r5, r5, #1
                                  0xe35004ff,    //  1054:	e35004ff 	cmp	r0, #-16777216	; 0xff000000
                                  0x2a000000,    //  1058:	2a000000 	bcs	1060 <label7>	
                                  0xe2855001,    //  105c:	e2855001 	add	r5, r5, #1	
                                  0xe3500003,    //  1060:	e3500003 	cmp	r0, #3	
                                  0x3a000000,    //  1064:	3a000000 	bcc	106c <label8>	
                                  0xe2855001,    //  1068:	e2855001 	add	r5, r5, #1	
                                  0xe35004ff,    //  106c:	e35004ff 	cmp	r0, #-16777216	; 0xff000000	
                                  0x3a000000,    //  1070:	3a000000 	bcc	1078 <label9>	
                                  0xe2855001,    //  1074:	e2855001 	add	r5, r5, #1	
                                  0xe3500007,    //  1078:	e3500007 	cmp	r0, #7	
                                  0x4a000000,    //  107c:	4a000000 	bmi	1084 <label10>	
                                  0xe2855001,    //  1080:	e2855001 	add	r5, r5, #1	
                                  0xe3500003,    //  1084:	e3500003 	cmp	r0, #3	
                                  0x4a000000,    //  1088:	4a000000 	bmi	1090 <label11>	
                                  0xe2855001,    //  108c:	e2855001 	add	r5, r5, #1	
                                  0xe3500007,    //  1090:	e3500007 	cmp	r0, #7	
                                  0x5a000000,    //  1094:	5a000000 	bpl	109c <label12>	
                                  0xe2855001,    //  1098:	e2855001 	add	r5, r5, #1	
                                  0xe3500003,    //  109c:	e3500003 	cmp	r0, #3	
                                  0x5a000000,    //  10a0:	5a000000 	bpl	10a8 <label13>	
                                  0xe2855001,    //  10a4:	e2855001 	add	r5, r5, #1	
                                  0xe3a02106,    //  10a8:	e3a02106 	mov	r2, #-2147483647	; 0x80000001
                                  0xe3500003,    //  10ac:	e3500003 	cmp	r0, #3	
                                  0x6a000000,    //  10b0:	6a000000 	bvs	10b8 <label14>	
                                  0xe2855001,    //  10b4:	e2855001 	add	r5, r5, #1	
                                  0xe1500002,    //  10b8:	e1500002 	cmp	r0, r2	
                                  0x6a000000,    //  10bc:	6a000000 	bvs	10c4 <label15>	
                                  0xe2855001,    //  10c0:	e2855001 	add	r5, r5, #1	
                                  0xe3500003,    //  10c4:	e3500003 	cmp	r0, #3	
                                  0x7a000000,    //  10c8:	7a000000 	bvc	10d0 <label16>	
                                  0xe2855001,    //  10cc:	e2855001 	add	r5, r5, #1	
                                  0xe1500002,    //  10d0:	e1500002 	cmp	r0, r2	
                                  0x7a000000,    //  10d4:	7a000000 	bvc	10dc <label17>	
                                  0xe2855001,    //  10d8:	e2855001 	add	r5, r5, #1	
                                  0xe3500003,    //  10dc:	e3500003 	cmp	r0, #3	
                                  0x8a000000,    //  10e0:	8a000000 	bhi	10e8 <label18>	
                                  0xe2855001,    //  10e4:	e2855001 	add	r5, r5, #1	
                                  0xe3500005,    //  10e8:	e3500005 	cmp	r0, #5	
                                  0x8a000000,    //  10ec:	8a000000 	bhi	10f4 <label19>	
                                  0xe2855001,    //  10f0:	e2855001 	add	r5, r5, #1	
                                  0xe3500003,    //  10f4:	e3500003 	cmp	r0, #3	
                                  0x9a000000,    //  10f8:	9a000000 	bls	1100 <label20>	
                                  0xe2855001,    //  10fc:	e2855001 	add	r5, r5, #1	
                                  0xe3500005,    //  1100:	e3500005 	cmp	r0, #5	
                                  0x9a000000,    //  1104:	9a000000 	bls	110c <label21>	
                                  0xe2855001,    //  1108:	e2855001 	add	r5, r5, #1	
                                  0xe3500003,    //  110c:	e3500003 	cmp	r0, #3	
                                  0xaa000000,    //  1110:	aa000000 	bge	1118 <label22>	
                                  0xe2855001,    //  1114:	e2855001 	add	r5, r5, #1	
                                  0xe3500007,    //  1118:	e3500007 	cmp	r0, #7	
                                  0xaa000000,    //  111c:	aa000000 	bge	1124 <label23>	
                                  0xe2855001,    //  1120:	e2855001 	add	r5, r5, #1	
                                  0xe3500007,    //  1124:	e3500007 	cmp	r0, #7	
                                  0xba000000,    //  1128:	ba000000 	blt	1130 <label24>	
                                  0xe2855001,    //  112c:	e2855001 	add	r5, r5, #1	
                                  0xe3500003,    //  1130:	e3500003 	cmp	r0, #3	
                                  0xba000000,    //  1134:	ba000000 	blt	113c <label25>	
                                  0xe2855001,    //  1138:	e2855001 	add	r5, r5, #1	
                                  0xe3500003,    //  113c:	e3500003 	cmp	r0, #3	
                                  0xca000000,    //  1140:	ca000000 	bgt	1148 <label26>	
                                  0xe2855001,    //  1144:	e2855001 	add	r5, r5, #1	
                                  0xe3500005,    //  1148:	e3500005 	cmp	r0, #5	
                                  0xca000000,    //  114c:	ca000000 	bgt	1154 <label27>	
                                  0xe2855001,    //  1150:	e2855001 	add	r5, r5, #1	
                                  0xe3500003,    //  1154:	e3500003 	cmp	r0, #3	
                                  0xda000000,    //  1158:	da000000 	ble	1160 <label28>	
                                  0xe2855001,    //  115c:	e2855001 	add	r5, r5, #1	
                                  0xe3500005,    //  1160:	e3500005 	cmp	r0, #5
                                  0xda000000,    //  1164:	da000000 	ble	116c <label29>
                                  0xe2855001,    //  1168:	e2855001 	add	r5, r5, #1
                                  0xeb000000,    //  116c:	eb000000 	bl	1174 <mysub>
                                  0xef000000,    //  1170:	ef000000 	svc	0x00000000
                                  0xe12fff1e     //  1174:	e12fff1e 	bx	lr
                             };

            // writing instruction into memory starting at 0x1130
            int len = instLst.Length;
            for (uint i = 0, addr = 0x1000; i < len; i++)
            {
                mem.WriteWord(addr, instLst[i]);
                addr += 4;
            }
            

            //  Strings of decoded instructions are in this format:      115c:	ebffffa7 	bl	1000 
            //      Addr    Instruct
            //     1000:	e3a05000 	mov	r5, #0
            cpu.fetch();
            cpu.decode();
            cpu.execute();
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            disLst.Add(curInstStr);
            Debug.Assert(regs.getRegNValue(5) == 0);
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1000: e3a05000 mov r5, #0");

            //     1004: e3a00000 mov r0, #0
            cpu.fetch();
            cpu.decode();
            cpu.execute();
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            disLst.Add(curInstStr);
            Debug.Assert(regs.getRegNValue(0) == 0);
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1004: e3a00000 mov r0, #0");

            //     1008: e3a01001 mov r1, #1
            cpu.fetch();
            cpu.decode();
            cpu.execute();
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            disLst.Add(curInstStr);
            Debug.Assert(regs.getRegNValue(1) == 1);
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1008: e3a01001 mov r1, #1");

            //     100c: ea000000 b 1014 // jump to instruction 1014
            cpu.fetch();
            cpu.decode();
            cpu.execute();
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            disLst.Add(curInstStr);
           // Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "100c: ea000000 b 1014");

            //    //     1014:	e1500000 cmp	r0, r0
            //    cpu.fetch();
            //    cpu.decode();
            //    cpu.execute();
            //    curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            //    disLst.Add(curInstStr);
            //    Console.WriteLine(regs.getRegNValue(5));
            //    Debug.Assert(regs.getRegNValue(5) == 1);
            //    Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1010: e2855001 add r5, r5, #1");

            cpu.traceLogFlush();
            cpu.traceLogClose();

            return;
        }

        private static void start_core_pointers_exe_tests()
        {
            Memory mem = new Memory(); // 32 KB
            Registers regs = new Registers();
            CPU cpu = new CPU(mem, regs);
            string curInstStr = ""; // facilitates debugging
            List<string> disLst = new List<string>(); // instruction disassembled list
            regs.updateProgramCounter(0x1000);   // set PC to 0x1000
            regs.updateStackPointer(0x00007000); // set SP to 0x00007000

            /******************************* load instruction set 1 to memory ********************************/
            //                    INSTRUCTION         ADDRESS           DISASSEMBLED
            uint[] instLst = {    0xe92d0810,    //  1000:	e92d0810 	push	{r4, fp}
                                  0xe28db004,    //  1004:	e28db004 	add	fp, sp, #4
                                  0xe24dd050,    //  1008:	e24dd050 	sub	sp, sp, #80	; 0x50
                                  0xe59f314c,    //  100c:	e59f314c 	ldr	r3, [pc, #332]	; 1160 <_start+0x160>
                                  0xe24bc050,    //  1010:	e24bc050 	sub	ip, fp, #80	; 0x50
                                  0xe1a04003,    //  1014:	e1a04003 	mov	r4, r3
                                  0xe8b4000f,    //  1018:	e8b4000f 	ldm	r4!, {r0, r1, r2, r3}
                                  0xe8ac000f,    //  101c:	e8ac000f 	stmia	ip!, {r0, r1, r2, r3}
                                  0xe8b4000f,    //  1020:	e8b4000f 	ldm	r4!, {r0, r1, r2, r3}
                                  0xe8ac000f,    //  1024:	e8ac000f 	stmia	ip!, {r0, r1, r2, r3}
                                  0xe8b4000f,    //  1028:	e8b4000f 	ldm	r4!, {r0, r1, r2, r3}
                                  0xe8ac000f,    //  102c:	e8ac000f 	stmia	ip!, {r0, r1, r2, r3}
                                  0xe8940007,    //  1030:	e8940007 	ldm	r4, {r0, r1, r2}
                                  0xe88c0007,    //  1034:	e88c0007 	stm	ip, {r0, r1, r2}
                                  0xe3a03000,    //  1038:	e3a03000 	mov	r3, #0
                                  0xe50b3010,    //  103c:	e50b3010 	str	r3, [fp, #-16]
                                  0xe3a0300f,    //  1040:	e3a0300f 	mov	r3, #15
                                  0xe50b3014,    //  1044:	e50b3014 	str	r3, [fp, #-20]	; 0xffffffec
                                  0xe24b3050,    //  1048:	e24b3050 	sub	r3, fp, #80	; 0x50
                                  0xe50b3008,    //  104c:	e50b3008 	str	r3, [fp, #-8]
                                  0xe1a00000,    //  1050:	e1a00000 	nop			; (mov r0, r0)
                                  0xe51b3008,    //  1054:	e51b3008 	ldr	r3, [fp, #-8]
                                  0xe5933000,    //  1058:	e5933000 	ldr	r3, [r3]
                                  0xe3530000,    //  105c:	e3530000 	cmp	r3, #0
                                  0x03a03000,    //  1060:	03a03000 	moveq	r3, #0
                                  0x13a03001,    //  1064:	13a03001 	movne	r3, #1
                                  0xe20330ff,    //  1068:	e20330ff 	and	r3, r3, #255	; 0xff
                                  0xe51b2008,    //  106c:	e51b2008 	ldr	r2, [fp, #-8]
                                  0xe2822004,    //  1070:	e2822004 	add	r2, r2, #4
                                  0xe50b2008,    //  1074:	e50b2008 	str	r2, [fp, #-8]
                                  0xe3530000,    //  1078:	e3530000 	cmp	r3, #0
                                  0x1afffff4,    //  107c:	1afffff4 	bne	1054 <_start+0x54>
                                  0xe24b3050,    //  1080:	e24b3050 	sub	r3, fp, #80	; 0x50
                                  0xe50b3008,    //  1084:	e50b3008 	str	r3, [fp, #-8]
                                  0xe51b3008,    //  1088:	e51b3008 	ldr	r3, [fp, #-8]
                                  0xe2833004,    //  108c:	e2833004 	add	r3, r3, #4
                                  0xe50b3008,    //  1090:	e50b3008 	str	r3, [fp, #-8]
                                  0xe51b3008,    //  1094:	e51b3008 	ldr	r3, [fp, #-8]
                                  0xe5933000,    //  1098:	e5933000 	ldr	r3, [r3]
                                  0xe3530000,    //  109c:	e3530000 	cmp	r3, #0
                                  0x1afffff8,    //  10a0:	1afffff8 	bne	1088 <_start+0x88>
                                  0xe3a03000,    //  10a4:	e3a03000 	mov	r3, #0
                                  0xe50b300c,    //  10a8:	e50b300c 	str	r3, [fp, #-12]
                                  0xea00000d,    //  10ac:	ea00000d 	b	10e8 <_start+0xe8>
                                  0xe51b300c,    //  10b0:	e51b300c 	ldr	r3, [fp, #-12]
                                  0xe3e0104b,    //  10b4:	e3e0104b 	mvn	r1, #75	; 0x4b
                                  0xe1a03103,    //  10b8:	e1a03103 	lsl	r3, r3, #2
                                  0xe24b0004,    //  10bc:	e24b0004 	sub	r0, fp, #4
                                  0xe0802003,    //  10c0:	e0802003 	add	r2, r0, r3
                                  0xe1a03001,    //  10c4:	e1a03001 	mov	r3, r1
                                  0xe0823003,    //  10c8:	e0823003 	add	r3, r2, r3
                                  0xe5933000,    //  10cc:	e5933000 	ldr	r3, [r3]
                                  0xe51b2010,    //  10d0:	e51b2010 	ldr	r2, [fp, #-16]
                                  0xe0823003,    //  10d4:	e0823003 	add	r3, r2, r3
                                  0xe50b3010,    //  10d8:	e50b3010 	str	r3, [fp, #-16]
                                  0xe51b300c,    //  10dc:	e51b300c 	ldr	r3, [fp, #-12]
                                  0xe2833001,    //  10e0:	e2833001 	add	r3, r3, #1
                                  0xe50b300c,    //  10e4:	e50b300c 	str	r3, [fp, #-12]
                                  0xe51b200c,    //  10e8:	e51b200c 	ldr	r2, [fp, #-12]
                                  0xe51b3014,    //  10ec:	e51b3014 	ldr	r3, [fp, #-20]	; 0xffffffec
                                  0xe1520003,    //  10f0:	e1520003 	cmp	r2, r3
                                  0xbaffffed,    //  10f4:	baffffed 	blt	10b0 <_start+0xb0>
                                  0xe3a03000,    //  10f8:	e3a03000 	mov	r3, #0
                                  0xe50b300c,    //  10fc:	e50b300c 	str	r3, [fp, #-12]
                                  0xea00000d,    //  1100:	ea00000d 	b	113c <_start+0x13c>
                                  0xe51b300c,    //  1104:	e51b300c 	ldr	r3, [fp, #-12]
                                  0xe2833001,    //  1108:	e2833001 	add	r3, r3, #1
                                  0xe50b300c,    //  110c:	e50b300c 	str	r3, [fp, #-12]
                                  0xe51b300c,    //  1110:	e51b300c 	ldr	r3, [fp, #-12]
                                  0xe3e0104b,    //  1114:	e3e0104b 	mvn	r1, #75	; 0x4b
                                  0xe1a03103,    //  1118:	e1a03103 	lsl	r3, r3, #2
                                  0xe24b0004,    //  111c:	e24b0004 	sub	r0, fp, #4
                                  0xe0802003,    //  1120:	e0802003 	add	r2, r0, r3
                                  0xe1a03001,    //  1124:	e1a03001 	mov	r3, r1
                                  0xe0823003,    //  1128:	e0823003 	add	r3, r2, r3
                                  0xe5933000,    //  112c:	e5933000 	ldr	r3, [r3]
                                  0xe51b2010,    //  1130:	e51b2010 	ldr	r2, [fp, #-16]
                                  0xe0823003,    //  1134:	e0823003 	add	r3, r2, r3
                                  0xe50b3010,    //  1138:	e50b3010 	str	r3, [fp, #-16]
                                  0xe51b200c,    //  113c:	e51b200c 	ldr	r2, [fp, #-12]
                                  0xe51b3014,    //  1140:	e51b3014 	ldr	r3, [fp, #-20]	; 0xffffffec
                                  0xe1520003,    //  1144:	e1520003 	cmp	r2, r3
                                  0xbaffffed,    //  1148:	baffffed 	blt	1104 <_start+0x104>
                                  0xef000000,    //  114c:	ef000000 	svc	0x00000000
                                  0xe1a00003,    //  1150:	e1a00003 	mov	r0, r3
                                  0xe24bd004,    //  1154:	e24bd004 	sub	sp, fp, #4
                                  0xe8bd0810,    //  1158:	e8bd0810 	pop	{r4, fp}
                                  0xe12fff1e,    //  115c:	e12fff1e 	bx	lr
                                  0x00001164     //  1160:	00001164 	.word	0x00001164
                             };

            // writing instruction into memory starting at 0x1130
            int len = instLst.Length;
            for (uint i = 0, addr = 0x1000; i < len; i++)
            {
                mem.WriteWord(addr, instLst[i]);
                addr += 4;
            }


            //  Strings of decoded instructions are in this format:      115c:	ebffffa7 	bl	1000 
            //      Addr    Instruct
            //     1000:   e92d0810 push	{r4, fp}    // sp = 0x7000
            cpu.fetch();
            cpu.decode();
            cpu.execute(); 
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            disLst.Add(curInstStr);
            Debug.Assert(regs.getRegNValue(4) == 0);
            Debug.Assert(regs.getRegNValue(11) == 0);
            Debug.Assert(mem.ReadWord(0x7000-4) == 0);
            Debug.Assert(mem.ReadWord(0x7000 - 8) == 0);
            Debug.Assert(regs.getRegNValue(13) == 0x7000 - 8);
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1000: e92d0810 push sp!, {r4, fp}");

            //     1004: e28db004 add fp, sp, #4
            cpu.fetch();
            cpu.decode();
            cpu.execute(); 
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            disLst.Add(curInstStr);
            Debug.Assert(regs.getRegNValue(13) == 0x7000 - 8);
            Debug.Assert(regs.getRegNValue(11) == 0x7000 - 8 + 4);
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1004: e28db004 add fp, sp, #4");

            //     1008: e24dd050 sub sp, sp, #80	; 0x50
            cpu.fetch();
            cpu.decode();
            cpu.execute();
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            disLst.Add(curInstStr);
            Debug.Assert(regs.getRegNValue(13) == 0x7000 - 8 - 80);
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1008: e24dd050 sub sp, sp, #80");

            
            //     100c:    e59f314c ldr	r3, [pc, #332]  // EA = 0x1160
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            disLst.Add(curInstStr);
            Debug.Assert(regs.getRegNValue(15) == 0x100c+4);
            Debug.Assert(regs.getRegNValue(3) == mem.ReadWord(0x100c + 8 + 332) );
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "100c: e59f314c ldr r3, [pc, #332]");

            
            // 1010: e24bc050 sub	ip, fp, #80	; 0x50 //
            cpu.fetch();
            cpu.decode();
            cpu.execute();
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            disLst.Add(curInstStr);
            Debug.Assert(regs.getRegNValue(11) == 0x7000 - 4);
            Debug.Assert(regs.getRegNValue(12) == regs.getRegNValue(11) - 80);
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1010: e24bc050 sub ip, fp, #80");

            // 1014: e1a04003 mov r4, r3
            cpu.fetch();
            cpu.decode();
            cpu.execute();
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            disLst.Add(curInstStr);
           
            // 1018: e8b4000f ldm r4!, {r0, r1, r2, r3} // r4 = 0x1164
            cpu.fetch();
            cpu.decode();
            cpu.execute();
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            disLst.Add(curInstStr);
            Debug.Assert(regs.getRegNValue(0) == mem.ReadWord(0x1164));
            Debug.Assert(regs.getRegNValue(1) == mem.ReadWord(0x1164+4));
            Debug.Assert(regs.getRegNValue(2) == mem.ReadWord(0x1164+8));
            Debug.Assert(regs.getRegNValue(3) == mem.ReadWord(0x1164+12));
            Debug.Assert(regs.getRegNValue(4) == 0x1164 + 16);
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1018: e8b4000f ldm r4!, {r0, r1, r2, r3}");

            // 101c: e8ac000f stmia	ip!, {r0, r1, r2, r3} // ip = 6fac
            //Console.WriteLine(regs.getRegNValue(12).ToString("X"));
            cpu.fetch();
            cpu.decode();
            cpu.execute(); // after execute r12ip = 6fbc
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            disLst.Add(curInstStr);
            //Console.WriteLine(regs.getRegNValue(12).ToString("X"));
            Debug.Assert(mem.ReadWord(0x6fac) == regs.getRegNValue(0));
            Debug.Assert(mem.ReadWord(0x6fb0) == regs.getRegNValue(1));
            Debug.Assert(mem.ReadWord(0x6fb4) == regs.getRegNValue(2));
            Debug.Assert(mem.ReadWord(0x6fb8) == regs.getRegNValue(3));
            Debug.Assert(0x6fbc == regs.getRegNValue(12));
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "101c: e8ac000f stmia ip!, {r0, r1, r2, r3}");

            // 1020: e8b4000f ldm r4!, {r0, r1, r2, r3}  // r4! before: 0x1174 after: 0x1184
            //Console.WriteLine(regs.getRegNValue(4).ToString("X"));
            cpu.fetch();
            cpu.decode();
            cpu.execute(); 
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            disLst.Add(curInstStr);
            //Console.WriteLine(regs.getRegNValue(4).ToString("X"));
            Debug.Assert(mem.ReadWord(0x1174-4) == regs.getRegNValue(3));
            Debug.Assert(mem.ReadWord(0x1174) == regs.getRegNValue(2));
            Debug.Assert(mem.ReadWord(0x1178) == regs.getRegNValue(1));
            Debug.Assert(mem.ReadWord(0x117c) == regs.getRegNValue(0));
            Debug.Assert(0x1184 == regs.getRegNValue(4));
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1020: e8b4000f ldm r4!, {r0, r1, r2, r3}");


            // 1024: e8ac000f stmia	ip!, {r0, r1, r2, r3}  // r12ip! before: 0x6fbc after: 0x6fcc



            //  uint r10sl;
            //  uint r11fp;
            //  uint r12ip; // frame pointer
            //  uint r13sp; // stack poointer
            //  uint r14lr; // link register, holds the caller's return address
            //  uint r15pc; // program counter

            cpu.traceLogFlush();
            cpu.traceLogClose();

            return;
        }

        private static void start_core_locals_exe_tests()
        {
            Memory mem = new Memory(); // 32 KB
            Registers regs = new Registers();
            CPU cpu = new CPU(mem, regs);
            string curInstStr = ""; // facilitates debugging
            regs.updateProgramCounter(0x1130); // set PC to 0x1000
            regs.updateStackPointer(0x00007000); // set SP to 0x00007000

            /******************************* load instruction set 1 to memory ********************************/
            //                    INSTRUCTION         ADDRESS           DISASSEMBLED
            uint[] instLst = {    0xe92d4800,     //  1130:	e92d4800 	push	{fp, lr}
                                  0xe28db004,     //  1134:	e28db004 	add	fp, sp, #4
                                  0xe24dd010,     //  1138:	e24dd010 	sub	sp, sp, #16
                                  0xe3a03032,     //  113c:	e3a03032 	mov	r3, #50	; 0x32
                                  0xe58d3000,     //  1140:	e58d3000 	str	r3, [sp]
                                  0xe3a0303c,     //  1144:	e3a0303c 	mov	r3, #60	; 0x3c
                                  0xe58d3004,     //  1148:	e58d3004 	str	r3, [sp, #4]
                                  0xe3a0000a,     //  114c:	e3a0000a 	mov	r0, #10
                                  0xe3a01014,     //  1150:	e3a01014 	mov	r1, #20
                                  0xe3a0201e,     //  1154:	e3a0201e 	mov	r2, #30
                                  0xe3a03028,     //  1158:	e3a03028 	mov	r3, #40	; 0x28
                                  0xebffffa7,     //  115c:	ebffffa7 	bl	1000 <sub>
                                  0xe50b0008,     //  1160:	e50b0008 	str	r0, [fp, #-8]
                                  0xef000000,     //  1164:	ef000000 	svc	0x00000000
                                  0xe1a00003,     //  1168:	e1a00003 	mov	r0, r3
                                  0xe24bd004,     //  116c:	e24bd004 	sub	sp, fp, #4
                                  0xe8bd4800,     //  1170:	e8bd4800 	pop	{fp, lr}
                                  0xe12fff1e      //  1174:	e12fff1e 	bx	lr
                             };

            // writing instruction into memory starting at 0x1130
            int len = instLst.Length;
            for (uint i = 0, addr = 0x1130; i < len; i++)
            {
                mem.WriteWord(addr, instLst[i]);
                addr += 4;
            }

            /******************************* load instruction set 1 to memory ********************************/
            //                    INSTRUCTION         ADDRESS           DISASSEMBLED
            uint[] instLst2 = {   0xe92d0820,     //  1000:	e92d0820 	push	{r5, fp}
                                  0xe28db004,     //  1004:	e28db004 	add	fp, sp, #4
                                  0xe24dd048,     //  1008:	e24dd048 	sub	sp, sp, #72	; 0x48
                                  0xe50b0040,     //  100c:	e50b0040 	str	r0, [fp, #-64]	; 0xffffffc0
                                  0xe50b1044,     //  1010:	e50b1044 	str	r1, [fp, #-68]	; 0xffffffbc
                                  0xe50b2048,     //  1014:	e50b2048 	str	r2, [fp, #-72]	; 0xffffffb8
                                  0xe50b304c,     //  1018:	e50b304c 	str	r3, [fp, #-76]	; 0xffffffb4
                                  0xe3a03001,     //  101c:	e3a03001 	mov	r3, #1
                                  0xe50b3008,     //  1020:	e50b3008 	str	r3, [fp, #-8]
                                  0xe3a03002,     //  1024:	e3a03002 	mov	r3, #2
                                  0xe50b300c,     //  1028:	e50b300c 	str	r3, [fp, #-12]
                                  0xe3a03003,     //  102c:	e3a03003 	mov	r3, #3
                                  0xe50b3010,     //  1030:	e50b3010 	str	r3, [fp, #-16]
                                  0xe3a03004,     //  1034:	e3a03004 	mov	r3, #4
                                  0xe50b3014,     //  1038:	e50b3014 	str	r3, [fp, #-20]	; 0xffffffec
                                  0xe3a03005,     //  103c:	e3a03005 	mov	r3, #5
                                  0xe50b3018,     //  1040:	e50b3018 	str	r3, [fp, #-24]	; 0xffffffe8
                                  0xe3a03006,     //  1044:	e3a03006 	mov	r3, #6
                                  0xe50b301c,     //  1048:	e50b301c 	str	r3, [fp, #-28]	; 0xffffffe4
                                  0xe3a03007,     //  104c:	e3a03007 	mov	r3, #7
                                  0xe50b3020,     //  1050:	e50b3020 	str	r3, [fp, #-32]	; 0xffffffe0
                                  0xe3a03008,     //  1054:	e3a03008 	mov	r3, #8
                                  0xe50b3024,     //  1058:	e50b3024 	str	r3, [fp, #-36]	; 0xffffffdc
                                  0xe3a03009,     //  105c:	e3a03009 	mov	r3, #9
                                  0xe50b3028,     //  1060:	e50b3028 	str	r3, [fp, #-40]	; 0xffffffd8
                                  0xe3a0300a,     //  1064:	e3a0300a 	mov	r3, #10
                                  0xe50b302c,     //  1068:	e50b302c 	str	r3, [fp, #-44]	; 0xffffffd4
                                  0xe3a0300b,     //  106c:	e3a0300b 	mov	r3, #11
                                  0xe50b3030,     //  1070:	e50b3030 	str	r3, [fp, #-48]	; 0xffffffd0
                                  0xe3a0300c,     //  1074:	e3a0300c 	mov	r3, #12
                                  0xe50b3034,     //  1078:	e50b3034 	str	r3, [fp, #-52]	; 0xffffffcc
                                  0xe3a0300d,     //  107c:	e3a0300d 	mov	r3, #13
                                  0xe50b3038,     //  1080:	e50b3038 	str	r3, [fp, #-56]	; 0xffffffc8
                                  0xe51b2008,     //  1084:	e51b2008 	ldr	r2, [fp, #-8]
                                  0xe51b300c,     //  1088:	e51b300c 	ldr	r3, [fp, #-12]
                                  0xe0822003,     //  108c:	e0822003 	add	r2, r2, r3
                                  0xe51b3010,     //  1090:	e51b3010 	ldr	r3, [fp, #-16]
                                  0xe0822003,     //  1094:	e0822003 	add	r2, r2, r3
                                  0xe51b3014,     //  1098:	e51b3014 	ldr	r3, [fp, #-20]	; 0xffffffec
                                  0xe0822003,     //  109c:	e0822003 	add	r2, r2, r3
                                  0xe51b3018,     //  10a0:	e51b3018 	ldr	r3, [fp, #-24]	; 0xffffffe8
                                  0xe0822003,     //  10a4:	e0822003 	add	r2, r2, r3
                                  0xe51b301c,     //  10a8:	e51b301c 	ldr	r3, [fp, #-28]	; 0xffffffe4
                                  0xe0822003,     //  10ac:	e0822003 	add	r2, r2, r3
                                  0xe51b3020,     //  10b0:	e51b3020 	ldr	r3, [fp, #-32]	; 0xffffffe0
                                  0xe0822003,     //  10b4:	e0822003 	add	r2, r2, r3
                                  0xe51b3024,     //  10b8:	e51b3024 	ldr	r3, [fp, #-36]	; 0xffffffdc
                                  0xe0822003,     //  10bc:	e0822003 	add	r2, r2, r3
                                  0xe51b3028,     //  10c0:	e51b3028 	ldr	r3, [fp, #-40]	; 0xffffffd8
                                  0xe0822003,     //  10c4:	e0822003 	add	r2, r2, r3
                                  0xe51b302c,     //  10c8:	e51b302c 	ldr	r3, [fp, #-44]	; 0xffffffd4
                                  0xe0822003,     //  10cc:	e0822003 	add	r2, r2, r3
                                  0xe51b3030,     //  10d0:	e51b3030 	ldr	r3, [fp, #-48]	; 0xffffffd0
                                  0xe0822003,     //  10d4:	e0822003 	add	r2, r2, r3
                                  0xe51b3034,     //  10d8:	e51b3034 	ldr	r3, [fp, #-52]	; 0xffffffcc
                                  0xe0822003,     //  10dc:	e0822003 	add	r2, r2, r3
                                  0xe51b3038,     //  10e0:	e51b3038 	ldr	r3, [fp, #-56]	; 0xffffffc8
                                  0xe0822003,     //  10e4:	e0822003 	add	r2, r2, r3
                                  0xe51b304c,     //  10e8:	e51b304c 	ldr	r3, [fp, #-76]	; 0xffffffb4
                                  0xe0822003,     //  10ec:	e0822003 	add	r2, r2, r3
                                  0xe59b3004,     //  10f0:	e59b3004 	ldr	r3, [fp, #4]
                                  0xe0822003,     //  10f4:	e0822003 	add	r2, r2, r3
                                  0xe59b3008,     //  10f8:	e59b3008 	ldr	r3, [fp, #8]
                                  0xe0822003,     //  10fc:	e0822003 	add	r2, r2, r3
                                  0xe51b3048,     //  1100:	e51b3048 	ldr	r3, [fp, #-72]	; 0xffffffb8
                                  0xe0822003,     //  1104:	e0822003 	add	r2, r2, r3
                                  0xe51b3044,     //  1108:	e51b3044 	ldr	r3, [fp, #-68]	; 0xffffffbc
                                  0xe0822003,     //  110c:	e0822003 	add	r2, r2, r3
                                  0xe51b3040,     //  1110:	e51b3040 	ldr	r3, [fp, #-64]	; 0xffffffc0
                                  0xe0823003,     //  1114:	e0823003 	add	r3, r2, r3
                                  0xe50b303c,     //  1118:	e50b303c 	str	r3, [fp, #-60]	; 0xffffffc4
                                  0xe51b303c,     //  111c:	e51b303c 	ldr	r3, [fp, #-60]	; 0xffffffc4
                                  0xe1a00003,     //  1120:	e1a00003 	mov	r0, r3
                                  0xe24bd004,     //  1124:	e24bd004 	sub	sp, fp, #4
                                  0xe8bd0820,     //  1128:	e8bd0820 	pop	{r5, fp}
                                  0xe12fff1e      //  112c:	e12fff1e 	bx	lr
                              };

            // writing instruction into memory starting at 0x1130
            int len2 = instLst2.Length;
            for (uint i = 0, addr = 0x1000; i < len2; i++)
            {
                mem.WriteWord(addr, instLst2[i]);
                addr += 4;
            }

            // execute 11 instructions to later test BL
            for (uint i = 0; i < 11; i++)
            {
                cpu.fetch();
                cpu.decode();
                cpu.execute(); //adds a instruction string to CPUInstructionRowList
            }



            //  Strings of decoded instructions are in this format:      115c:	ebffffa7 	bl	1000 
            //      Addr    Instruct
            //      115c: ebffffa7 bl	1000   
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            Debug.Assert(regs.getRegNValue(15) == 0x1000);
            Debug.Assert(regs.getRegNValue(14) == 0x1160);
           // Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "115c: ebffffa7 bl 1000");

            // execute 75 instructions to later test BX
            for (uint i = 0; i < 75; i++)
            {
                cpu.fetch();
                cpu.decode();
                cpu.execute(); //adds a instruction string to CPUInstructionRowList
            }

            // 112c: e12fff1e bx lr
            Debug.Assert(regs.getRegNValue(15) == 0x112c);
            cpu.fetch();
            Debug.Assert(regs.getRegNValue(15) == 0x112c + 4);
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            Debug.Assert(regs.getRegNValue(15) == 0x1160);
            Debug.Assert(regs.getRegNValue(14) == 0x1160);
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "112c: e12fff1e bx lr");


            // execute 5 instructions to later test BX
            for (uint i = 0; i < 5; i++)
            {
                cpu.fetch();
                cpu.decode();
                cpu.execute(); //adds a instruction string to CPUInstructionRowList
            }

            // 1174: e12fff1e bx	lr
            Debug.Assert(regs.getRegNValue(15) == 0x1174);
            cpu.fetch();
            Debug.Assert(regs.getRegNValue(15) == 0x1174 + 4);
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            Debug.Assert(regs.getRegNValue(12) == 0);
            Debug.Assert(regs.getRegNValue(14) == 0);
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1174: e12fff1e bx lr");


            cpu.turnOffTraceLog();


            return;
        }

        private static void start_core_cmp_exe_tests()
        {
            Memory mem = new Memory(); // 32 KB
            Registers regs = new Registers();
            CPU cpu = new CPU(mem, regs);
            string curInstStr = ""; // facilitates debugging
            regs.updateProgramCounter(0x1000); // set PC to 0x1000
            regs.updateStackPointer(0x00007000); // set SP to 0x00007000

            //                    INSTRUCTION         ADDRESS           DISASSEMBLED
            uint[] instLst = {    0xe3a00005,     //  1000:	e3a00005 	mov	r0, #5
                                  0xe3500003,     //  1004:	e3500003 	cmp	r0, #3
                                  0xe35004ff,     //  1008:	e35004ff 	cmp	r0, #-16777216	; 0xff000000
                                  0xe350047f,     //  100c:	e350047f 	cmp	r0, #2130706432	; 0x7f000000
                                  0xe3500007,     //  1010:	e3500007 	cmp	r0, #7
                                  0xe3a004fe,     //  1014:	e3a004fe 	mov	r0, #-33554432	; 0xfe000000
                                  0xe3500003,     //  1018:	e3500003 	cmp	r0, #3
                                  0xe35004ff,     //  101c:	e35004ff 	cmp	r0, #-16777216	; 0xff000000
                                  0xe3a00005,     //  1020:	e3a00005 	mov	r0, #5
                                  0xe3e01002,     //  1024:	e3e01002 	mvn	r1, #2
                                  0xe3a02106,     //  1028:	e3a02106 	mov	r2, #-2147483647	; 0x80000001
                                  0xe3e03003,     //  102c:	e3e03003 	mvn	r3, #3
                                  0xe3500003,     //  1030:	e3500003 	cmp	r0, #3
                                  0xe3500007,     //  1034:	e3500007 	cmp	r0, #7
                                  0xe1500001,     //  1038:	e1500001 	cmp	r0, r1
                                  0xe1500002,     //  103c:	e1500002 	cmp	r0, r2
                                  0xe1520000,     //  1040:	e1520000 	cmp	r2, r0
                                  0xe1510000,     //  1044:	e1510000 	cmp	r1, r0
                                  0xe1510003,     //  1048:	e1510003 	cmp	r1, r3
                                  0xe1530001,     //  104c:	e1530001 	cmp	r3, r1
                                  0xe1530003,     //  1050:	e1530003 	cmp	r3, r3
                                  0xef000000      //  1054:	ef000000 	svc	0x00000000
                             };

            // writing instruction into memory starting at 0x1000
            int len = instLst.Length;
            for (uint i = 0, addr = 0x1000; i < len; i++)
            {
                mem.WriteWord(addr, instLst[i]);
                addr += 4;
            }

            //  Strings of decoded instructions are in this format:      0x00001000: e3a00fb5: mov r0, #724
            //      Addr    Instruct
            //    1000:	e3a00005 	mov	r0, #5      
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 5);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001000: e3a00005: mov r0, #5");

            //    1004:	e3500003 	cmp	r0, #3
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 5);
            Debug.Assert(regs.getNFlag() == 0);
            Debug.Assert(regs.getZFlag() == 0);
            Debug.Assert(regs.getCFlag() == 1);
            Debug.Assert(regs.getVFlag() == 0);
            curInstStr = cpu.getTopInstAddrDisString();
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001004: e3500003: cmp r0, #3");

            //    1008:	e35004ff 	cmp	r0, #-16777216	; 0xff000000
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 5);
            Debug.Assert(regs.getNFlag() == 0);
            Debug.Assert(regs.getZFlag() == 0);
            Debug.Assert(regs.getCFlag() == 0);
            Debug.Assert(regs.getVFlag() == 0);
            curInstStr = cpu.getTopInstAddrDisString();
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001008: e35004ff: cmp r0, #-16777216");

            //    100c:	e350047f 	cmp	r0, #2130706432	; 0x7f000000
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 5);
            Debug.Assert(regs.getNFlag() == 1);
            Debug.Assert(regs.getZFlag() == 0);
            Debug.Assert(regs.getCFlag() == 0);
            Debug.Assert(regs.getVFlag() == 0);
            curInstStr = cpu.getTopInstAddrDisString();
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x0000100C: e350047f: cmp r0, #2130706432");

            //    1010:	e3500007 	cmp	r0, #7
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 5);
            Debug.Assert(regs.getNFlag() == 1);
            Debug.Assert(regs.getZFlag() == 0);
            Debug.Assert(regs.getCFlag() == 0);
            Debug.Assert(regs.getVFlag() == 0);
            curInstStr = cpu.getTopInstAddrDisString();
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001010: e3500007: cmp r0, #7");

            //    1014:	e3a004fe 	mov	r0, #-33554432	; 0xfe000000
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 0xfe000000);
            curInstStr = cpu.getTopInstAddrDisString();
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001014: e3a004fe: mov r0, #-33554432");

            //    1018:	e3500003 	cmp	r0, #3
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 0xfe000000); // r0 = #-33554432	; 0xfe000000
            Debug.Assert(regs.getNFlag() == 1);
            Debug.Assert(regs.getZFlag() == 0);
            Debug.Assert(regs.getCFlag() == 1);
            Debug.Assert(regs.getVFlag() == 0);
            curInstStr = cpu.getTopInstAddrDisString();
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001018: e3500003: cmp r0, #3");

            //    101c:	e35004ff 	cmp	r0, #-16777216	; 0xff000000
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 0xfe000000); // r0 = #-33554432	; 0xfe000000
            Debug.Assert(regs.getNFlag() == 1);
            Debug.Assert(regs.getZFlag() == 0);
            Debug.Assert(regs.getCFlag() == 0);
            Debug.Assert(regs.getVFlag() == 0);
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "101c: e35004ff cmp r0, #-16777216");

            //1020: e3a00005 mov r0, #5
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 5); 
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1020: e3a00005 mov r0, #5");

            //1024: e3e01002 mvn r1, #2
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(1) == ~(uint)2);
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1024: e3e01002 mvn r1, #2");

            //1028: e3a02106 mov r2, #-2147483647	; 0x80000001
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(2) == 0x80000001);
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1028: e3a02106 mov r2, #-2147483647");

            //102c: e3e03003 mvn r3, #3
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(3) == ~(uint)3);
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "102c: e3e03003 mvn r3, #3");

            //1030: e3500003 cmp r0, #3
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 5); // r0 = #-33554432	; 0xfe000000
            Debug.Assert(regs.getNFlag() == 0);
            Debug.Assert(regs.getZFlag() == 0);
            Debug.Assert(regs.getCFlag() == 1);
            Debug.Assert(regs.getVFlag() == 0);
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1030: e3500003 cmp r0, #3");

            //    1034: e3500007 cmp r0, #7
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 5); 
            Debug.Assert(regs.getNFlag() == 1);
            Debug.Assert(regs.getZFlag() == 0);
            Debug.Assert(regs.getCFlag() == 0);
            Debug.Assert(regs.getVFlag() == 0);
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1034: e3500007 cmp r0, #7");

            //1038: e1500001 cmp r0, r1 // r1 = ~2 = 0xffff fffd = -3 = 4294967293 // r0 - r1 = 5 - -3
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 5);
            Debug.Assert(regs.getRegNValue(1) == 0xfffffffd);
            Debug.Assert(regs.getNFlag() == 0);
            Debug.Assert(regs.getZFlag() == 0);
            Debug.Assert(regs.getCFlag() == 0);
            Debug.Assert(regs.getVFlag() == 0);
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1038: e1500001 cmp r0, r1");

            //103c: e1500002 cmp r0, r2 // r2 = #-2147483647	; 0x80000001
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 5);
            Debug.Assert(regs.getRegNValue(2) == 0x80000001);
            Debug.Assert(regs.getNFlag() == 1);
            Debug.Assert(regs.getZFlag() == 0);
            Debug.Assert(regs.getCFlag() == 0);
            Debug.Assert(regs.getVFlag() == 1);
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "103c: e1500002 cmp r0, r2");


            //1040: e1520000 cmp r2, r0 // r2 - r0 = (0x80000001 or -2147483647)  - 5
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 5);
            Debug.Assert(regs.getRegNValue(2) == 0x80000001);
            Debug.Assert(regs.getNFlag() == 0);
            Debug.Assert(regs.getZFlag() == 0);
            Debug.Assert(regs.getCFlag() == 1);
            Debug.Assert(regs.getVFlag() == 1);
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1040: e1520000 cmp r2, r0");

            //1044: e1510000 cmp r1, r0 // r1 - r0 = -3  - 5
            // r1 = ~2 = 0xffff fffd = -3 = 4294967293
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 5);
            Debug.Assert(regs.getRegNValue(1) == 0xfffffffd);
            Debug.Assert(regs.getNFlag() == 1);
            Debug.Assert(regs.getZFlag() == 0);
            Debug.Assert(regs.getCFlag() == 1);
            Debug.Assert(regs.getVFlag() == 0);
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1044: e1510000 cmp r1, r0");

            //    1048: e1510003 cmp r1, r3
            //    r1 = ~2 = 0xfffffffd = -3 = 4294967293
            //    r3 = ~3 = 0xfffffffc = -4 = 4294967292
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(3) == 0xfffffffc);
            Debug.Assert(regs.getRegNValue(1) == 0xfffffffd);
            Debug.Assert(regs.getNFlag() == 0);
            Debug.Assert(regs.getZFlag() == 0);
            Debug.Assert(regs.getCFlag() == 1);
            Debug.Assert(regs.getVFlag() == 0);
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1048: e1510003 cmp r1, r3");

            //    104c: e1530001 cmp r3, r1
            //    r3 = ~3 = 0xfffffffc = -4 = 4294967292
            //    r1 = ~2 = 0xfffffffd = -3 = 4294967293
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(3) == 0xfffffffc);
            Debug.Assert(regs.getRegNValue(1) == 0xfffffffd);
            Debug.Assert(regs.getNFlag() == 1);
            Debug.Assert(regs.getZFlag() == 0);
            Debug.Assert(regs.getCFlag() == 0);
            Debug.Assert(regs.getVFlag() == 0);
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "104c: e1530001 cmp r3, r1");

            //    1050: e1530003 cmp r3, r3
            //    r3 = ~3 = 0xfffffffc = -4 = 4294967292
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(3) == 0xfffffffc);
            Debug.Assert(regs.getNFlag() == 0);
            Debug.Assert(regs.getZFlag() == 1);
            Debug.Assert(regs.getCFlag() == 1);
            Debug.Assert(regs.getVFlag() == 0);
            curInstStr = cpu.getTopInstAddrDisStringNotepadFormat();
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1050: e1530003 cmp r3, r3");

            //1054: ef000000 svc 0x00000000
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(cpu.getTopInstAddrDisStringNotepadFormat() == "1054: ef000000 svc 0x00000000");

            cpu.traceLogFlush();
            cpu.traceLogClose();

            // Helper code to test
            //Console.WriteLine(cpu.getCPUInstructionRowListLastItem()); /********************************************/
            //Console.WriteLine(regs.getRegNValue(15));                  /********************************************/
            //Console.WriteLine(cpu.getCPUCurrentAddr() + 4);            /********************************************/

            // helper2
            //Console.WriteLine(regs.getRegNValue(3).ToString("x"));
            //Console.WriteLine((int)regs.getRegNValue(3));
            //Console.WriteLine(regs.getRegNValue(3));
            return;
        }


    }
}
