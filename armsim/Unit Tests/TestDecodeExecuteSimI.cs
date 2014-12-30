using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace armsim
{
	 public static class  TestDecodeExecuteSimI
	{
		public static void runTests()
		{
            Console.WriteLine("Testing TestDecodeExecuteSimI class methods");
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("Testing startCVersionTests()");
            startCVersionTests();

            Console.WriteLine("Testing startBVersionTests()");
            startBVersionTests();

            return;

		}


        private static void startCVersionTests()
        {

            Memory mem = new Memory(); // 32 KB
            Registers regs = new Registers();
            CPU cpu = new CPU(mem, regs);


            //************************************ C Version Tests ********************************//
            //************************************ testing MOV *********************************//

            // At memory slot 0
            // WriteWord(memAddress, Value)
            mem.WriteWord(0, 0xe3a02030); // e3a02030 = MOV R2, #48
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(2) == 48);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00000000: e3a02030: mov r2, #48"); // a whole instruction string gets saved in a one row

            // At memory slot 4
            mem.WriteWord(4, 0xe1a0476d); // 0xe1a0476d = MOV R4, R13, ROR #14
            regs.updateRegisterN(13, 1);
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(4) == BarrelShifter.ror(1, 14));
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00000004: e1a0476d: mov r4, sp, ror #14");



            //************************************ testing MVN *********************************//

            // At memory slot 8
            mem.WriteWord(8, 0xe1e09442); // 0xe1e09442 = MVN R9, R2, ASR #8
            regs.updateRegisterN(2, 2);
            regs.updateRegisterN(9, 9);
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(9) == ~(BarrelShifter.asr(2, 8)));
            Debug.Assert(regs.getRegNValue(2) == 2);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00000008: e1e09442: mvn r9, r2, asr #8");

            // At memory slot 12
            mem.WriteWord(12, 0xE1E0D18B); // 0xE1E0D18B = MVN R13, R11, LSL #3
            regs.updateRegisterN(13, 13);
            regs.updateRegisterN(11, 11);
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(13) == ~(BarrelShifter.lsl(11, 3)));
            Debug.Assert(regs.getRegNValue(11) == 11);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x0000000C: e1e0d18b: mvn sp, fp, lsl #3");

            // At memory slot 16
            mem.WriteWord(16, 0xE3E0E0FF); // 0xE3E0E0FF = MVN R14, #255
            regs.updateRegisterN(14, 14);
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(14) == ~((uint)255));
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00000010: e3e0e0ff: mvn lr, #255");


            //************************************ testing ADD *********************************//

            // At memory slot 20
            mem.WriteWord(20, 0XE28100FF); // 0XE28100FF = ADD RO, R1, #255 // Immediate
            regs.updateRegisterN(0, 0);
            regs.updateRegisterN(1, 1);
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 256);
            Debug.Assert(regs.getRegNValue(1) == 1);

            //Console.WriteLine("0x00000014\t ADD RO, R1, #255");
            //Console.WriteLine(cpu.getCPUInstructionRowListLastItem());
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00000014: e28100ff: add r0, r1, #255");



            //************************************ Tests for ctests.exe  ********************************//

            // set up for test cases
            mem.clearMemory();
            regs.clearRegisters();
            cpu.clearInstAddrDisLists();


            //                    INSTRUCTION         ADDRESS           DISASSEMBLED
            uint[] instLst = {    0xe3a00fb5,     //  1000:	e3a00fb5 	mov	r0, #724	; 0x2d4
                                  0xe3a014a1,     //  1004:	e3a014a1 	mov	r1, #-1593835520	; 0xa1000000
                                  0xe1a02000,     //  1008:	e1a02000 	mov	r2, r0
                                  0xe1a0200f,     //  100c:	e1a0200f 	mov	r2, pc
                                  0xe1a02141,     //  1010:	e1a02141 	asr	r2, r1, #2
                                  0xe1a02121,     //  1014:	e1a02121 	lsr	r2, r1, #2
                                  0xe1a02081,     //  1018:	e1a02081 	lsl	r2, r1, #1
                                  0xe1a02260,     //  101c:	e1a02260 	ror	r2, r0, #4
                                  0xe3e03001,     //  1020:	e3e03001 	mvn	r3, #1
                                  0xe3a04004,     //  1024:	e3a04004 	mov	r4, #4
                                  0xe2445003,     //  1028:	e2445003 	sub	r5, r4, #3
                                  0xe2445003,     //  102c:	e2445003 	sub	r5, r4, #3
                                  0xe2645003,     //  1030:	e2645003 	rsb	r5, r4, #3
                                  0xe20020ff,     //  1034:	e20020ff 	and	r2, r0, #255	; 0xff
                                  0xe3802012,     //  1038:	e3802012 	orr	r2, r0, #18
                                  0xe2202fb7,     //  103c:	e2202fb7 	eor	r2, r0, #732	; 0x2dc
                                  0xe20020ff,     //  1040:	e20020ff 	and	r2, r0, #255	; 0xff
                                  0xe3a02002,     //  1044:	e3a02002 	mov	r2, #2
                                  0xe0050291,     //  1048:	e0050291 	mul	r5, r1, r2
                                  0xef000011      //  104c:	ef000011 	svc	0x00000011
                            };

            // writing instruction into memory starting at 0x1000
            for (uint i = 0, addr = 0x1000; i < instLst.Length; i++)
            {
                mem.WriteWord(addr, instLst[i]);
                addr += 4;
            }

            regs.updateProgramCounter(0x1000); // set PC


            //************************************ MOV ********************************//

            //  Strings of decoded instructions are in this format:      1000: e3a00fb5 \tmov r0, #724
            //      Addr    Instruct
            //      1000:	e3a00fb5 	mov	r0, #724	; 0x2d4
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 724);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001000: e3a00fb5: mov r0, #724");

            //      1004:	e3a014a1 	mov	r1, #-1593835520	; 0xa1000000
            regs.updateRegisterN(1, 1);
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(1) == 0xa1000000);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001004: e3a014a1: mov r1, #-1593835520");

            //      1008:	e1a02000 	mov	r2, r0
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 724);
            Debug.Assert(regs.getRegNValue(2) == 724);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001008: e1a02000: mov r2, r0");

            //      100c:	e1a0200f 	mov	r2, pc
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(2) == cpu.getCPUCurrentAddr() + 8);
            Debug.Assert(regs.getRegNValue(15) == cpu.getCPUCurrentAddr() + 4);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x0000100C: e1a0200f: mov r2, pc");

            //      1010:	e1a02141 	asr	r2, r1, #2  = mov r2, r1, asr #2
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(1) == 0xa1000000);
            Debug.Assert(regs.getRegNValue(2) == BarrelShifter.asr(0xa1000000, 2));
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001010: e1a02141: mov r2, r1, asr #2");

            //       1014:	e1a02121 	lsr	r2, r1, #2  = mov r2, r1, lsr #2
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(1) == 0xa1000000);
            Debug.Assert(regs.getRegNValue(2) == BarrelShifter.lsr(0xa1000000, 2));
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001014: e1a02121: mov r2, r1, lsr #2");

            //       1018:	e1a02081 	lsl	r2, r1, #1  = mov r2, r1, lsl #1
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(1) == 0xa1000000);
            Debug.Assert(regs.getRegNValue(2) == BarrelShifter.lsl(0xa1000000, 1));
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001018: e1a02081: mov r2, r1, lsl #1");

            //       101c:	e1a02260 	ror	r2, r0, #4  = mov r2, r0, ror #4
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 724);
            Debug.Assert(regs.getRegNValue(2) == BarrelShifter.ror(724, 4));
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x0000101C: e1a02260: mov r2, r0, ror #4");

            //    1020:	e3e03001 	mvn	r3, #1
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(3) == ~(uint)1);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001020: e3e03001: mvn r3, #1");

            //    1024:	e3a04004 	mov	r4, #4
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(4) == 4);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001024: e3a04004: mov r4, #4");

            //    1028:	e2445003 	sub	r5, r4, #3
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(4) == 4);
            Debug.Assert(regs.getRegNValue(5) == 1);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001028: e2445003: sub r5, r4, #3");

            //    102c:	e2445003 	sub	r5, r4, #3
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(4) == 4);
            Debug.Assert(regs.getRegNValue(5) == 1);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x0000102C: e2445003: sub r5, r4, #3");

            //    1030:	e2645003 	rsb	r5, r4, #3
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(4) == 4);
            Debug.Assert((int)regs.getRegNValue(5) == 3 - 4);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001030: e2645003: rsb r5, r4, #3");

            //    1034:	e20020ff 	and	r2, r0, #255  ; 0xff
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 0x2d4);
            Debug.Assert(regs.getRegNValue(2) == 0xd4);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001034: e20020ff: and r2, r0, #255");

            //    1038:	e3802012 	orr	r2, r0, #18
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 0x2d4);
            Debug.Assert(regs.getRegNValue(2) == 0x2d6);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001038: e3802012: orr r2, r0, #18");

            //    103c:	e2202fb7 	eor	r2, r0, #732	; 0x2dc
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 0x2d4);
            Debug.Assert(regs.getRegNValue(2) == 0x8);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x0000103C: e2202fb7: eor r2, r0, #732");

            //    1040:	e20020ff 	and	r2, r0, #255	; 0xff
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(0) == 0x2d4);
            Debug.Assert(regs.getRegNValue(2) == 0xd4);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001040: e20020ff: and r2, r0, #255");

            //    1044:	e3a02002 	mov	r2, #2
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(2) == 2);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001044: e3a02002: mov r2, #2");

            //    1048:	e0050291 	mul	r5, r1, r2
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(2) == 2);
            Debug.Assert(regs.getRegNValue(1) == 0xa1000000);
            uint n1 = 0xa1000000;
            uint n2 = 2;
            Debug.Assert(regs.getRegNValue(5) == n1 * n2);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001048: e0050291: mul r5, r1, r2");

            //    104c:	ef000011 	svc	0x00000011
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(2) == 2);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x0000104C: ef000011: svc 0x00000011");


            cpu.traceLogFlush();
            cpu.traceLogClose();

            // Helper code to test
            //Console.WriteLine(cpu.getCPUInstructionRowListLastItem()); /********************************************/
            //Console.WriteLine(regs.getRegNValue(15));                  /********************************************/
            //Console.WriteLine(cpu.getCPUCurrentAddr() + 4);            /********************************************/
            return;
        }

        private static void startBVersionTests()
        {
            Memory mem = new Memory(); // 32 KB
            Registers regs = new Registers();
            CPU cpu = new CPU(mem, regs);
            regs.updateProgramCounter(0x1000); // set PC to 0x1000
            regs.updateStackPointer(0x00007000); // set SP to 0x00007000

            //                    INSTRUCTION         ADDRESS           DISASSEMBLED
            uint[] instLst = {   0xe3a01efb,     //  1000:	e3a01efb 	mov	r1, #4016	; 0xfb0     // 00001000 <_start>:
                                 0xe3a02a05,     //  1004:	e3a02a05 	mov	r2, #20480	; 0x5000
                                 0xe3a03a03,     //  1008:	e3a03a03 	mov	r3, #12288	; 0x3000
                                 0xe3a04008,     //  100c:	e3a04008 	mov	r4, #8
                                 0xe5821000,     //  1010:	e5821000 	str	r1, [r2]
                                 0xe5021004,     //  1014:	e5021004 	str	r1, [r2, #-4]
                                 0xe7021004,     //  1018:	e7021004 	str	r1, [r2, -r4]
                                 0xe7821004,     //  101c:	e7821004 	str	r1, [r2, r4]
                                 0xe78210c4,     //  1020:	e78210c4 	str	r1, [r2, r4, asr #1]
                                 0xe7821104,     //  1024:	e7821104 	str	r1, [r2, r4, lsl #2]
                                 0xe5c2100c,     //  1028:	e5c2100c 	strb	r1, [r2, #12]
                                 0xe5925000,     //  102c:	e5925000 	ldr	r5, [r2]
                                 0xe5126004,     //  1030:	e5126004 	ldr	r6, [r2, #-4]
                                 0xe7127004,     //  1034:	e7127004 	ldr	r7, [r2, -r4]
                                 0xe7928004,     //  1038:	e7928004 	ldr	r8, [r2, r4]
                                 0xe79290c4,     //  103c:	e79290c4 	ldr	r9, [r2, r4, asr #1]
                                 0xe792a104,     //  1040:	e792a104 	ldr	sl, [r2, r4, lsl #2]
                                 0xe5d2b00c,     //  1044:	e5d2b00c 	ldrb	fp, [r2, #12]
                                 0xe3a01001,     //  1048:	e3a01001 	mov	r1, #1
                                 0xe3a02002,     //  104c:	e3a02002 	mov	r2, #2
                                 0xe3a04004,     //  1050:	e3a04004 	mov	r4, #4
                                 0xe92d0016,     //  1054:	e92d0016 	push	{r1, r2, r4}
                                 0xe3a0100a,     //  1058:	e3a0100a 	mov	r1, #10
                                 0xe3a0301e,     //  105c:	e3a0301e 	mov	r3, #30
                                 0xe92d000a,     //  1060:	e92d000a 	push	{r1, r3}
                                 0xe3a01000,     //  1064:	e3a01000 	mov	r1, #0
                                 0xe3a03000,     //  1068:	e3a03000 	mov	r3, #0
                                 0xe8bd000a,     //  106c:	e8bd000a 	pop	{r1, r3}
                                 0xe89d0016,     //  1070:	e89d0016 	ldm	sp, {r1, r2, r4}
                                 0xef000011      //  1074:	ef000011 	svc	0x00000011
                             };


            // writing instruction into memory starting at 0x1000
            for (uint i = 0, addr = 0x1000; i < instLst.Length; i++)
            {
                mem.WriteWord(addr, instLst[i]);
                addr += 4;
            }

            //  Strings of decoded instructions are in this format:      0x00001000: e3a00fb5: mov r0, #724
            //      Addr    Instruct
            //     1000:	e3a01efb 	mov	r1, #4016	; 0xfb0
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(1) == 0xfb0);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001000: e3a01efb: mov r1, #4016");

            //    1004:	e3a02a05 	mov	r2, #20480	; 0x5000
           // regs.updateRegisterN(1, 1);
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(2) == 0x5000);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001004: e3a02a05: mov r2, #20480");

            //    1008:	e3a03a03 	mov	r3, #12288	; 0x3000
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(3) == 0x3000);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001008: e3a03a03: mov r3, #12288");

            //    100c:	e3a04008 	mov	r4, #8
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(4) == 8);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x0000100C: e3a04008: mov r4, #8");

            //     1010:	e5821000 	str	r1, [r2]
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(1) == 0xfb0);
            Debug.Assert(regs.getRegNValue(2) == 0x5000);
            Debug.Assert(mem.ReadWord(0x5000) == 0xfb0);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001010: e5821000: str r1, [r2]");

            //    1014:	e5021004 	str	r1, [r2, #-4]
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(1) == 0xfb0);
            Debug.Assert(regs.getRegNValue(2) == 0x5000);
            Debug.Assert(mem.ReadWord(0x4FFC) == 0xfb0);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001014: e5021004: str r1, [r2, #-4]");

            //    1018:	e7021004 	str	r1, [r2, -r4]
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(1) == 0xfb0);
            Debug.Assert(regs.getRegNValue(2) == 0x5000);
            Debug.Assert(regs.getRegNValue(4) == 8);
            Debug.Assert(mem.ReadWord(20480 - 4) == 0xfb0);
            Debug.Assert(mem.ReadWord(20480 - 8) == 0xfb0);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001018: e7021004: str r1, [r2, -r4]");

            //    101c:	e7821004 	str	r1, [r2, r4]
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(1) == 0xfb0);
            Debug.Assert(regs.getRegNValue(2) == 0x5000);
            Debug.Assert(regs.getRegNValue(4) == 8);
            Debug.Assert(mem.ReadWord(0x5000 + 0x8) == 0xfb0);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x0000101C: e7821004: str r1, [r2, r4]");

            //  1020:	e78210c4 	str	r1, [r2, r4, asr #1]
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(1) == 0xfb0);
            Debug.Assert(regs.getRegNValue(2) == 0x5000);
            Debug.Assert(regs.getRegNValue(4) == 8);
            Debug.Assert(mem.ReadWord(0x5000 + BarrelShifter.asr(8, 1) ) == 0xfb0);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001020: e78210c4: str r1, [r2, r4, asr #1]");

            //    1024:	e7821104 	str	r1, [r2, r4, lsl #2]
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(1) == 0xfb0);
            Debug.Assert(regs.getRegNValue(2) == 0x5000);
            Debug.Assert(regs.getRegNValue(4) == 8);
            Debug.Assert(mem.ReadWord(0x5000 + BarrelShifter.lsl(8, 2)) == 0xfb0);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001024: e7821104: str r1, [r2, r4, lsl #2]");

            //    1028:	e5c2100c 	strb	r1, [r2, #12]
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(1) == 0xfb0);
            Debug.Assert(regs.getRegNValue(2) == 0x5000);
            Debug.Assert(mem.ReadWord(0x5000 + 0xC) == 0xb0);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001028: e5c2100c: strb r1, [r2, #12]");

            //    102c:	e5925000 	ldr	r5, [r2]
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(2) == 0x5000);
            Debug.Assert(regs.getRegNValue(5) == mem.ReadWord(0x5000) );
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x0000102C: e5925000: ldr r5, [r2]");

            //    1030:	e5126004 	ldr	r6, [r2, #-4]
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(2) == 0x5000);
            Debug.Assert(regs.getRegNValue(6) == mem.ReadWord(0x5000 - 4));
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001030: e5126004: ldr r6, [r2, #-4]");

            //    1034:	e7127004 	ldr	r7, [r2, -r4]
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(2) == 0x5000);
            Debug.Assert(regs.getRegNValue(4) == 8);
            Debug.Assert(regs.getRegNValue(7) == mem.ReadWord(0x5000 - 8));
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001034: e7127004: ldr r7, [r2, -r4]");

            //     1038:	e7928004 	ldr	r8, [r2, r4]
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(2) == 0x5000);
            Debug.Assert(regs.getRegNValue(4) == 8);
            Debug.Assert(regs.getRegNValue(8) == mem.ReadWord(0x5000 + 8));
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001038: e7928004: ldr r8, [r2, r4]");

            //    103c:	e79290c4 	ldr	r9, [r2, r4, asr #1]
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(2) == 0x5000);
            Debug.Assert(regs.getRegNValue(4) == 8);
            Debug.Assert(regs.getRegNValue(9) == mem.ReadWord(BarrelShifter.asr(8, 1) + 0x5000));
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x0000103C: e79290c4: ldr r9, [r2, r4, asr #1]");

            //    1040:	e792a104 	ldr	sl, [r2, r4, lsl #2]
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(2) == 0x5000);
            Debug.Assert(regs.getRegNValue(4) == 8);
            Debug.Assert(regs.getRegNValue(10) == mem.ReadWord(BarrelShifter.asr(8, 1) + 0x5000));
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001040: e792a104: ldr sl, [r2, r4, lsl #2]");

            //    1044:	e5d2b00c 	ldrb	fp, [r2, #12]
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(2) == 0x5000);
            Debug.Assert(regs.getRegNValue(11) == mem.ReadWord(12 + 0x5000));
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001044: e5d2b00c: ldrb fp, [r2, #12]");

            //    1048:	e3a01001 	mov	r1, #1
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(1) == 1);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001048: e3a01001: mov r1, #1");

            //    104c:	e3a02002 	mov	r2, #2
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(2) == 2);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x0000104C: e3a02002: mov r2, #2");

            //    1050:	e3a04004 	mov	r4, #4
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(4) == 4);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001050: e3a04004: mov r4, #4");

            //     1054:	e92d0016 	push	{r1, r2, r4}
            Debug.Assert(regs.getStackPointer() == 0x7000);
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getStackPointer() == 0x7000 - 12);
            Debug.Assert(mem.ReadWord(0x7000 - 4) == regs.getRegNValue(4) );
            Debug.Assert(mem.ReadWord(0x7000 - 8) == regs.getRegNValue(2));
            Debug.Assert(mem.ReadWord(0x7000 - 12) == regs.getRegNValue(1));
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001054: e92d0016: push sp!, {r1, r2, r4}");

            //    1058:	e3a0100a 	mov	r1, #10
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(1) == 10);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001058: e3a0100a: mov r1, #10");

            //    105c:	e3a0301e 	mov	r3, #30
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(3) == 30);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x0000105C: e3a0301e: mov r3, #30");

            //    1060:	e92d000a 	push	{r1, r3}
            Debug.Assert(regs.getStackPointer() == 0x7000 - 12);
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getStackPointer() == 0x7000 - 20);
            Debug.Assert(mem.ReadWord(0x7000 - 16) == regs.getRegNValue(3));
            Debug.Assert(mem.ReadWord(0x7000 - 20) == regs.getRegNValue(1));
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001060: e92d000a: push sp!, {r1, r3}");

            //    1064:	e3a01000 	mov	r1, #0
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(1) == 0);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001064: e3a01000: mov r1, #0");

            //    1068:	e3a03000 	mov	r3, #0
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(3) == 0);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001068: e3a03000: mov r3, #0");

            //    106c:	e8bd000a 	pop	{r1, r3}
            Debug.Assert(regs.getStackPointer() == 0x7000 - 20);
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getStackPointer() == 0x7000 - 12);
            Debug.Assert(regs.getRegNValue(1) == mem.ReadWord(0x7000 - 20));
            Debug.Assert(regs.getRegNValue(3) == mem.ReadWord(0x7000 - 16));
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x0000106C: e8bd000a: pop sp!, {r1, r3}");

            //     1070:	e89d0016 	ldm	sp, {r1, r2, r4}
            Debug.Assert(regs.getStackPointer() == 0x7000 - 12);
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getStackPointer() == 0x7000 - 12);
            Debug.Assert(regs.getRegNValue(1) == mem.ReadWord(0x7000 - 12));
            Debug.Assert(regs.getRegNValue(2) == mem.ReadWord(0x7000 - 8));
            Debug.Assert(regs.getRegNValue(4) == mem.ReadWord(0x7000 - 4));
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001070: e89d0016: ldm sp, {r1, r2, r4}");

            //    1074:	ef000011 	svc	0x00000011
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00001074: ef000011: svc 0x00000011");


            cpu.traceLogFlush();
            cpu.traceLogClose();

            // Helper code to test
            //Console.WriteLine(cpu.getCPUInstructionRowListLastItem()); /********************************************/
            //Console.WriteLine(regs.getRegNValue(15));                  /********************************************/
            //Console.WriteLine(cpu.getCPUCurrentAddr() + 4);            /********************************************/


            return;
        }


	}
}
