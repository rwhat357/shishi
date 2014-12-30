using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace armsim.Prototype
{
    public static class TestBarrelShifter
    {
        public static void runTests()
        {
            Console.WriteLine("Testing BarrelShifter class methods");
            Console.WriteLine("------------------------------");

            // use http://www.binaryhexconverter.com/decimal-to-hex-converter to convert between hex, dec, and bin

            Console.WriteLine("Testing: lsl(int instruct, int num)...");
            uint i1 = 0xffff;
            uint i2 = 0xf;
            uint i3 = 0xa;
            uint i4 = 0x1;
            uint i5 = 0xfffffff;
            Debug.Assert(BarrelShifter.lsl(i1, 2) == 0x3fffc);
            Debug.Assert(BarrelShifter.lsl(i1, 1) == 0x1fffe);
            Debug.Assert(BarrelShifter.lsl(i1, 4) == 0xffff0);
            Debug.Assert(BarrelShifter.lsl(i5, 2) == 0x3ffffffc);
            Debug.Assert(BarrelShifter.lsl(i4, 3) == 0x8);
            Debug.Assert(BarrelShifter.lsl(i3, 8) == 0xa00);
            Debug.Assert(BarrelShifter.lsl(i2, 6) == 0x3c0);
            i1 = 0xFFFFFFFE;            // 2's comp = FFFF FFFE   = -0x2;       
            i2 = 0xFFFFFFF1;            // 2's comp = FFFF FFF1   = -0xf;       
            i3 = 0xFFFFFFF6;            // 2's comp = FFFF FFF6   = -0xa;       
            i4 = 0xFF000100;            // 2's comp = FF00 0100   = -0xffff00;  
            i5 = 0xF0000001;            // 2's comp = F000 0001   = -0xfffffff; 
            Debug.Assert(BarrelShifter.lsl(i1, 2) == 0xfffffff8);
            Debug.Assert(BarrelShifter.lsl(i1, 1) == 0xfffffffc);
            Debug.Assert(BarrelShifter.lsl(i1, 8) == 0xfffffe00);
            Debug.Assert(BarrelShifter.lsl(i2, 4) == 0xffffff10);
            Debug.Assert(BarrelShifter.lsl(i3, 8) == 0xfffff600);
            Debug.Assert(BarrelShifter.lsl(i4, 8) == 0x00010000);
            Debug.Assert(BarrelShifter.lsl(i5, 12) == 0x00001000);
            Debug.Assert(BarrelShifter.lsl(i5, 31) == 0x80000000);
            Debug.Assert(BarrelShifter.lsl(i5, 32) == 0x00000000);
            Debug.Assert(BarrelShifter.lsl(i5, 100) == 0x00000000);
            

            Console.WriteLine("Testing: lsr(int instruct, int num)...");
            i1 = 0x2;
            i2 = 0xf;
            i3 = 0xa;
            i4 = 0xffff00;
            i5 = 0xfffffff;
            Debug.Assert(BarrelShifter.lsr(i1, 2) == 0x0);
            Debug.Assert(BarrelShifter.lsr(i1, 1) == 0x1);
            Debug.Assert(BarrelShifter.lsr(i1, 8) == 0x0);
            Debug.Assert(BarrelShifter.lsr(i2, 2) == 0x3);
            Debug.Assert(BarrelShifter.lsr(i3, 3) == 0x1);
            Debug.Assert(BarrelShifter.lsr(i4, 8) == 0xffff);
            Debug.Assert(BarrelShifter.lsr(i5, 6) == 0x3fffff);
            i1 = 0xFFFFFFFE;            // 2's comp = FFFF FFFE   = -0x2;       
            i2 = 0xFFFFFFF1;            // 2's comp = FFFF FFF1   = -0xf;       
            i3 = 0xFFFFFFF6;            // 2's comp = FFFF FFF6   = -0xa;       
            i4 = 0xFF000100;            // 2's comp = FF00 0100   = -0xffff00;  
            i5 = 0xF0000001;            // 2's comp = F000 0001   = -0xfffffff; 
            Debug.Assert(BarrelShifter.lsr(i1, 1) == 0x7fffffff);
            Debug.Assert(BarrelShifter.lsr(i1, 2) == 0x3fffffff);
            Debug.Assert(BarrelShifter.lsr(i1, 8) == 0x00ffffff);
            Debug.Assert(BarrelShifter.lsr(i2, 4) == 0x0fffffff);
            Debug.Assert(BarrelShifter.lsr(i2, 4) == 0x0fffffff);
            Debug.Assert(BarrelShifter.lsr(i3, 8) == 0x00ffffff);
            Debug.Assert(BarrelShifter.lsr(i4, 12) == 0x000ff000);
            Debug.Assert(BarrelShifter.lsr(i5, 16) == 0x0000f000);


            Console.WriteLine("Testing: asr(int instruct, int num)...");
            i1 = 0x2;
            i2 = 0xf;
            i3 = 0xa;
            i4 = 0xffff00;
            i5 = 0xfffffff;
            Debug.Assert(BarrelShifter.asr(i1, 1 ) == 0x1);
            Debug.Assert(BarrelShifter.asr(i1, 2 ) == 0x0);
            Debug.Assert(BarrelShifter.asr(i1, 8 ) == 0x0);
            Debug.Assert(BarrelShifter.asr(i2, 4 ) == 0x0);
            Debug.Assert(BarrelShifter.asr(i3, 4 ) == 0x0);
            Debug.Assert(BarrelShifter.asr(i4, 8 ) == 0xffff);
            Debug.Assert(BarrelShifter.asr(i5, 12) == 0xffff);
            i1 = 0xFFFFFFFE;            // 2's comp = FFFF FFFE   = -0x2;       
            i2 = 0xFFFFFFF1;            // 2's comp = FFFF FFF1   = -0xf;       
            i3 = 0xFFFFFFF6;            // 2's comp = FFFF FFF6   = -0xa;       
            i4 = 0xFF000100;            // 2's comp = FF00 0100   = -0xffff00;  
            i5 = 0xF0000001;            // 2's comp = F000 0001   = -0xfffffff; 
            Debug.Assert(BarrelShifter.asr(i1, 1) == 0xffffffff);
            Debug.Assert(BarrelShifter.asr(i1, 2) == 0xffffffff);
            Debug.Assert(BarrelShifter.asr(i1, 8) == 0xffffffff);
            Debug.Assert(BarrelShifter.asr(i2, 4) == 0xffffffff);
            Debug.Assert(BarrelShifter.asr(i2, 2) == 0xfffffffc);
            Debug.Assert(BarrelShifter.asr(i3, 4) == 0xffffffff);
            Debug.Assert(BarrelShifter.asr(i4, 8) == 0xffff0001);
            Debug.Assert(BarrelShifter.asr(i5, 12) == 0xffff0000);
            
           
            Console.WriteLine("Testing: asl(int instruct, int num)...");
            i1 = 0x2;
            i2 = 0xf;
            i3 = 0xa;
            i4 = 0xffff00;
            i5 = 0xfffffff;
            Debug.Assert(BarrelShifter.asl(i1, 2) == 0x8);
            Debug.Assert(BarrelShifter.asl(i1, 1) == 0x4);
            Debug.Assert(BarrelShifter.asl(i1, 8) == 0x200);
            Debug.Assert(BarrelShifter.asl(i2, 2) == 0x3c);
            Debug.Assert(BarrelShifter.asl(i3, 3) == 0x50);
            Debug.Assert(BarrelShifter.asl(i4, 8) == 0xffff0000);
            Debug.Assert(BarrelShifter.asl(i5, 4) == 0xfffffff0);
            i1 = 0xFFFFFFFE;            // 2's comp = FFFF FFFE   = -0x2;       
            i2 = 0xFFFFFFF1;            // 2's comp = FFFF FFF1   = -0xf;       
            i3 = 0xFFFFFFF6;            // 2's comp = FFFF FFF6   = -0xa;       
            i4 = 0xFF000100;            // 2's comp = FF00 0100   = -0xffff00;  
            i5 = 0xF0000001;            // 2's comp = F000 0001   = -0xfffffff; 
            Debug.Assert(BarrelShifter.asl(i1, 2) == 0xfffffff8);
            Debug.Assert(BarrelShifter.asl(i1, 1) == 0xfffffffc);
            Debug.Assert(BarrelShifter.asl(i1, 8) == 0xfffffe00);
            Debug.Assert(BarrelShifter.asl(i2, 4) == 0xffffff10);
            Debug.Assert(BarrelShifter.asl(i3, 8) == 0xfffff600);
            Debug.Assert(BarrelShifter.asl(i4, 8) == 0x00010000);
            Debug.Assert(BarrelShifter.asl(i5, 12) == 0x00001000);


            Console.WriteLine("Testing: ror(int instruct, int num)...");
            i1 = 0x2;
            i2 = 0xf;
            i3 = 0xa;
            i4 = 0x00ffff00;
            i5 = 0x0fffffff;
            Debug.Assert(BarrelShifter.ror(i1, 0) == 0x2);
            Debug.Assert(BarrelShifter.ror(i1, 4) == 0x20000000);
            Debug.Assert(BarrelShifter.ror(i1, 8) == 0x02000000);
            Debug.Assert(BarrelShifter.ror(i2, 8) == 0x0f000000);
            Debug.Assert(BarrelShifter.ror(i3, 4) == 0xa0000000);
            Debug.Assert(BarrelShifter.ror(i4, 12) == 0xf0000fff);
            Debug.Assert(BarrelShifter.ror(i5, 16) == 0xffff0fff);
            i1 = 0xFFFFFFFE;            // 2's comp = FFFF FFFE   = -0x2;       
            i2 = 0xFFFFFFF1;            // 2's comp = FFFF FFF1   = -0xf;       
            i3 = 0xFFFFFFF6;            // 2's comp = FFFF FFF6   = -0xa;       
            i4 = 0xFF000100;            // 2's comp = FF00 0100   = -0xffff00;  
            i5 = 0xF0000001;            // 2's comp = F000 0001   = -0xfffffff; 
            Debug.Assert(BarrelShifter.ror(i1, 0) == 0xfffffffe);
            Debug.Assert(BarrelShifter.ror(i1, 4) == 0xefffffff);
            Debug.Assert(BarrelShifter.ror(i1, 8) == 0xfeffffff);
            Debug.Assert(BarrelShifter.ror(i2, 4) == 0x1fffffff);
            Debug.Assert(BarrelShifter.ror(i3, 8) == 0xf6ffffff);
            Debug.Assert(BarrelShifter.ror(i4, 8) == 0x00ff0001);
            Debug.Assert(BarrelShifter.ror(i5, 12) == 0x001f0000);
            Debug.Assert(BarrelShifter.ror(i5, 32) == 0xf0000001);
            Debug.Assert(BarrelShifter.ror(i5, 64) == 0xf0000001);

        }
    }
}
