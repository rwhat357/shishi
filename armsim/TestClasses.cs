using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using NDesk.Options;
using System.Runtime.InteropServices;
using ELFSharp.ELF;
using System.Diagnostics; // for Trace() and Debug()


namespace armsim
{

    //**********************************************************Testing Classes********************************************************//
    public class TestMemory
    {
        public void runTests()
        {

            Memory sim = new Memory(12);
            byte[] data = new byte[] { 14, 1, 2, 255 };

            // load data to RAM
            for (uint i = 0; i < data.Length; i++)
                sim.WriteByte(i, data[i]);

            Console.WriteLine("Testing Memory class methods");
            Console.WriteLine("------------------------------");

            // test ints
            int wordNum1 = int.MinValue;
            int wordNum2 = -999;
            int wordNum3 = 0;
            int wordNum4 = 999;
            int wordNum5 = int.MaxValue;
            Console.WriteLine("Testing: WriteWord(uint address, int dataValue)...");
            Debug.Assert(sim.WriteWord(0, wordNum1) == 0);
            Debug.Assert(sim.WriteWord(1, wordNum2) == -1);
            Debug.Assert(sim.WriteWord(2, wordNum3) == -1);
            Debug.Assert(sim.WriteWord(3, wordNum4) == -1);
            Debug.Assert(sim.WriteWord(4, wordNum5) == 0);
            Debug.Assert(sim.WriteWord(5, wordNum1) == -1);
            Debug.Assert(sim.WriteWord(6, wordNum2) == -1);
            Debug.Assert(sim.WriteWord(7, wordNum3) == -1);
            Debug.Assert(sim.WriteWord(8, wordNum4) == 0);
            Debug.Assert(sim.WriteWord(9, wordNum5) == -1);
            Debug.Assert(sim.WriteWord(10, wordNum1) == -1);
            Debug.Assert(sim.WriteWord(11, wordNum2) == -1);
            Debug.Assert(sim.WriteWord(12, wordNum3) == -1);
            Debug.Assert(sim.WriteWord(13, wordNum4) == -1);
            Debug.Assert(sim.WriteWord(14, wordNum5) == -1);
            Debug.Assert(sim.WriteWord(15, wordNum1) == -1);
            Debug.Assert(sim.WriteWord(16, wordNum2) == -1);
            Console.WriteLine("Testing: test passed.");

            Console.WriteLine("Testing: ReadWord(uint address)...");
            Debug.Assert(sim.ReadWord(0) == wordNum1);
            Debug.Assert(sim.ReadWord(1) == int.MaxValue);
            Debug.Assert(sim.ReadWord(2) == int.MaxValue);
            Debug.Assert(sim.ReadWord(3) == int.MaxValue);
            Debug.Assert(sim.ReadWord(4) == wordNum5);
            Debug.Assert(sim.ReadWord(5) == int.MaxValue);
            Debug.Assert(sim.ReadWord(6) == int.MaxValue);
            Debug.Assert(sim.ReadWord(7) == int.MaxValue);
            Debug.Assert(sim.ReadWord(8) == wordNum4);
            Debug.Assert(sim.ReadWord(9) == int.MaxValue);
            Debug.Assert(sim.ReadWord(10) == int.MaxValue);
            Debug.Assert(sim.ReadWord(11) == int.MaxValue);
            Debug.Assert(sim.ReadWord(12) == int.MaxValue);
            Debug.Assert(sim.ReadWord(13) == int.MaxValue);
            Debug.Assert(sim.ReadWord(14) == int.MaxValue);
            Debug.Assert(sim.ReadWord(15) == int.MaxValue);
            Debug.Assert(sim.ReadWord(16) == int.MaxValue);
            Console.WriteLine("Testing: test passed.");


            // test shorts
            short shortNum1 = short.MinValue;
            short shortNum2 = 999;
            short shortNum3 = 0;
            short shortNum4 = -999;
            short shortNum5 = short.MaxValue;
            Console.WriteLine("Testing: WriteHalfWord(uint address, short dataValue)...");
            Debug.Assert(sim.WriteHalfWord(0, shortNum1) == 0);
            Debug.Assert(sim.WriteHalfWord(1, shortNum2) == -1);
            Debug.Assert(sim.WriteHalfWord(2, shortNum3) == 0);
            Debug.Assert(sim.WriteHalfWord(3, shortNum4) == -1);
            Debug.Assert(sim.WriteHalfWord(4, shortNum5) == 0);
            Debug.Assert(sim.WriteHalfWord(5, shortNum1) == -1);
            Debug.Assert(sim.WriteHalfWord(6, shortNum2) == 0);
            Debug.Assert(sim.WriteHalfWord(7, shortNum3) == -1);
            Debug.Assert(sim.WriteHalfWord(8, shortNum4) == 0);
            Debug.Assert(sim.WriteHalfWord(9, shortNum1) == -1);
            Debug.Assert(sim.WriteHalfWord(10, shortNum2) == 0);
            Debug.Assert(sim.WriteHalfWord(11, shortNum3) == -1);
            Debug.Assert(sim.WriteHalfWord(12, shortNum4) == -1);
            Debug.Assert(sim.WriteHalfWord(13, shortNum5) == -1);
            Debug.Assert(sim.WriteHalfWord(14, shortNum1) == -1);
            Debug.Assert(sim.WriteHalfWord(15, shortNum2) == -1);
            Debug.Assert(sim.WriteHalfWord(16, shortNum3) == -1);
            Console.WriteLine("Testing: test passed.");

            Console.WriteLine("Testing: ReadHalfWord(uint address)...");
            Debug.Assert(sim.ReadHalfWord(0) == shortNum1);
            Debug.Assert(sim.ReadHalfWord(1) == short.MaxValue);
            Debug.Assert(sim.ReadHalfWord(2) == shortNum3);
            Debug.Assert(sim.ReadHalfWord(3) == short.MaxValue);
            Debug.Assert(sim.ReadHalfWord(4) == shortNum5);
            Debug.Assert(sim.ReadHalfWord(5) == short.MaxValue);
            Debug.Assert(sim.ReadHalfWord(6) == shortNum2);
            Debug.Assert(sim.ReadHalfWord(7) == short.MaxValue);
            Debug.Assert(sim.ReadHalfWord(8) == shortNum4);
            Debug.Assert(sim.ReadHalfWord(9) == short.MaxValue);
            Debug.Assert(sim.ReadHalfWord(10) == shortNum2);
            Debug.Assert(sim.ReadHalfWord(11) == short.MaxValue);
            Debug.Assert(sim.ReadHalfWord(12) == short.MaxValue);
            Debug.Assert(sim.ReadHalfWord(13) == short.MaxValue);
            Debug.Assert(sim.ReadHalfWord(14) == short.MaxValue);
            Debug.Assert(sim.ReadHalfWord(15) == short.MaxValue);
            Debug.Assert(sim.ReadHalfWord(16) == short.MaxValue);
            Console.WriteLine("Testing: test passed.");


            // test bytes
            byte byteNum1 = byte.MinValue;
            byte byteNum2 = 1;
            byte byteNum3 = 10;
            byte byteNum4 = 200;
            byte byteNum5 = byte.MaxValue;
            Console.WriteLine("Testing: WriteByte(uint address, byte dataValue)...");
            Debug.Assert(sim.WriteByte(0, byteNum1) == 0);
            Debug.Assert(sim.WriteByte(1, byteNum2) == 0);
            Debug.Assert(sim.WriteByte(2, byteNum3) == 0);
            Debug.Assert(sim.WriteByte(3, byteNum4) == 0);
            Debug.Assert(sim.WriteByte(4, byteNum5) == 0);
            Debug.Assert(sim.WriteByte(5, byteNum1) == 0);
            Debug.Assert(sim.WriteByte(6, byteNum2) == 0);
            Debug.Assert(sim.WriteByte(7, byteNum3) == 0);
            Debug.Assert(sim.WriteByte(8, byteNum4) == 0);
            Debug.Assert(sim.WriteByte(9, byteNum5) == 0);
            Debug.Assert(sim.WriteByte(10, byteNum1) == 0);
            Debug.Assert(sim.WriteByte(11, byteNum2) == 0);
            Debug.Assert(sim.WriteByte(12, byteNum3) == -1);
            Debug.Assert(sim.WriteByte(13, byteNum4) == -1);
            Debug.Assert(sim.WriteByte(14, byteNum5) == -1);
            Debug.Assert(sim.WriteByte(15, byteNum1) == -1);
            Debug.Assert(sim.WriteByte(16, byteNum2) == -1);
            Console.WriteLine("Testing: test passed.");

            Console.WriteLine("Testing: ReadByte(uint address)...");
            Debug.Assert(sim.ReadByte(0) == byteNum1);
            Debug.Assert(sim.ReadByte(1) == byteNum2);
            Debug.Assert(sim.ReadByte(2) == byteNum3);
            Debug.Assert(sim.ReadByte(3) == byteNum4);
            Debug.Assert(sim.ReadByte(4) == byteNum5);
            Debug.Assert(sim.ReadByte(5) == byteNum1);
            Debug.Assert(sim.ReadByte(6) == byteNum2);
            Debug.Assert(sim.ReadByte(7) == byteNum3);
            Debug.Assert(sim.ReadByte(8) == byteNum4);
            Debug.Assert(sim.ReadByte(9) == byteNum5);
            Debug.Assert(sim.ReadByte(10) == byteNum1);
            Debug.Assert(sim.ReadByte(11) == byteNum2);
            Debug.Assert(sim.ReadByte(12) == byte.MaxValue);
            Debug.Assert(sim.ReadByte(13) == byte.MaxValue);
            Debug.Assert(sim.ReadByte(14) == byte.MaxValue);
            Debug.Assert(sim.ReadByte(15) == byte.MaxValue);
            Debug.Assert(sim.ReadByte(16) == byte.MaxValue);
            Console.WriteLine("Testing: test passed.");


            // test setFlag() and testFlag()
            // load and set up RAM for testing
            wordNum1 = -999;
            wordNum2 = -2;
            wordNum3 = 0;
            wordNum4 = 2;
            wordNum5 = 999;
            int wordNum6 = 1000;

            Console.WriteLine("Testing: TestFlag(uint address, int bit)...");

            sim.WriteWord(0, wordNum1);
            sim.WriteWord(4, wordNum2);
            sim.WriteWord(8, wordNum3);
            Debug.Assert(sim.TestFlag(0, 2) == false);
            Debug.Assert(sim.TestFlag(4, 4) == true);
            Debug.Assert(sim.TestFlag(8, 8) == false);

            sim.WriteWord(0, wordNum4);
            sim.WriteWord(4, wordNum5);
            sim.WriteWord(8, wordNum6);
            Debug.Assert(sim.TestFlag(0, 1) == true);
            Debug.Assert(sim.TestFlag(4, 31) == false);
            Debug.Assert(sim.TestFlag(8, 37) == true);
            Console.WriteLine("Testing: test passed.");

            Console.WriteLine("Testing: SetFlag(uint address, int bit, bool flag)...");
            Debug.Assert(sim.SetFlag(0, 0, true) == 0);
            Debug.Assert(sim.SetFlag(1, 1, false) == -1);
            Debug.Assert(sim.SetFlag(2, 2, true) == -1);
            Debug.Assert(sim.SetFlag(3, 3, false) == -1);
            Debug.Assert(sim.SetFlag(4, 4, true) == 0);
            Debug.Assert(sim.SetFlag(5, 5, false) == -1);
            Debug.Assert(sim.SetFlag(6, 6, true) == -1);
            Debug.Assert(sim.SetFlag(7, 7, false) == -1);
            Debug.Assert(sim.SetFlag(8, 31, false) == -1);
            Debug.Assert(sim.SetFlag(8, -1, false) == -1);
            Debug.Assert(sim.SetFlag(8, 0, false) == -1);
            Console.WriteLine("Testing: test passed.");

            return;
        }
    }

}