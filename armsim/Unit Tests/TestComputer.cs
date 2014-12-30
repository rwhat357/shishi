using System;
using System.Diagnostics; // Trace() and Debug()
using System.ComponentModel; // BackgroundWorker
using armsim; // my custom namespace

/*
public static class TestComputer
{
    Computer computer;
    BackgroundWorker worker = new BackgroundWorker();

    public static void runTests()
    {
        // Configure BackgroundWorker
        this.worker.WorkerSupportsCancellation = true;
        this.worker.DoWork += this.worker_DoWork;
        this.worker.RunWorkerCompleted += this.worker_RunWorkerCompleted;

        Console.WriteLine("Testing Computer class methods");
        Console.WriteLine("------------------------------");

        computer = new Computer(worker, 32768);

        Console.WriteLine("Testing: getMemChecksum()...");
        string hexDigest = computer.getMemChecksum();
        Debug.Assert(hexDigest == "bb7df04e1b0a2570657527a7e108ae23");

        Console.WriteLine("Testing: run()...");
        Debug.Assert(computer.run() == 0);

        Console.WriteLine("Testing: step()...");
        Debug.Assert(computer.step() == 1);

        Console.WriteLine("Testing: loadSegmentsIntoRAM()...");
        Debug.Assert(computer.loadSegmentsIntoRAM("sample.notexistent") == -1);
        Debug.Assert(computer.loadSegmentsIntoRAM(".notexistent") == -1);

        Console.WriteLine("Testing: isTraceLogEnabled()...");
        Debug.Assert(computer.isTraceLogEnabled() == true);
        computer.turnOffTraceLog();
        Debug.Assert(computer.isTraceLogEnabled() == false);
        computer.turnOnTraceLog();
        Debug.Assert(computer.isTraceLogEnabled() == true);
            
        Console.WriteLine("Testing: getRegistersIntArray()...");
        uint[] regIntArr = computer.getRegistersIntArray();
        Debug.Assert(regIntArr[0] == 0);
        Debug.Assert(regIntArr[14] == 0);

        Console.WriteLine("Testing: loadSegmentsIntoRAM()...");
        uint[] contFlags = computer.getControlFlagsIntArray();
        Debug.Assert(contFlags[0] == 0);
        Debug.Assert(contFlags[1] == 0);
        Debug.Assert(contFlags[2] == 0);
        Debug.Assert(contFlags[3] == 0);

        Console.WriteLine("Testing: getRegisterNames()...");
        string[] regNames = computer.getRegisterNames();
        Debug.Assert(regNames[0] == "R0");
        Debug.Assert(regNames[1] == "R1");
        Debug.Assert(regNames[2] == "R2");
        Debug.Assert(regNames[3] == "R3");
        Debug.Assert(regNames[4] == "R4");
        Debug.Assert(regNames[5] == "R5");
        Debug.Assert(regNames[6] == "R6");
        Debug.Assert(regNames[7] == "R7");
        Debug.Assert(regNames[8] == "R8");
        Debug.Assert(regNames[9] == "R9");
        Debug.Assert(regNames[10] == "R10");
        Debug.Assert(regNames[11] == "R11");
        Debug.Assert(regNames[12] == "FP");
        Debug.Assert(regNames[13] == "SP");
        Debug.Assert(regNames[14] == "LR");
        Debug.Assert(regNames[15] == "PC");

        Console.WriteLine("Testing: getFourWordsFromMemory...");
        uint[] fourWords = computer.getFourWordsFromMemory(0);
        Debug.Assert(fourWords[0] == 0);
        Debug.Assert(fourWords[1] == 0);
        Debug.Assert(fourWords[2] == 0);
        Debug.Assert(fourWords[3] == 0);

        return;
    }

    // Runs on background thread
    private void worker_DoWork(object sender, DoWorkEventArgs e)
    {
        // start running
        int runCheck = 1;
        runCheck = computer.run();
    }

    // Runs on GUI thread, after worker_DoWork() returns
    private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        //updatePanelsAndResetMenuItems();
    }
}
 */
