using System;
using System.Diagnostics; // Trace() and Debug()
using armsim; // my custom namespace


public static class TestCPU
{
    public static void runtTests()
    {
        Console.WriteLine("Testing CPU class methods");
        Console.WriteLine("------------------------------");

        Memory mem = new Memory();
        Registers reg = new Registers();

        CPU testCPU = new CPU(mem, reg);


        Console.WriteLine("Testing: fetch()...");
        /*Debug.Assert(testCPU.fetch() == 0);
        reg.updateProgramCounter(4);
        Debug.Assert(testCPU.fetch() == 0);*/

        Console.WriteLine("Testing: decodeLaS(int word)...");
        // nothing writing yet

        Console.WriteLine("Testing: executeLaS()...");
        // waits for three second

    }
}
