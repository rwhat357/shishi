using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices; //GCHandle convert byte array to struct
using System.Diagnostics; // for Trace() and Debug()
using System.Windows.Forms;
using armsim; // my custom namespace

public class Computer: Observer
{
    Subject subject;

    Memory memory;
    Registers registers;
    CPU cpu;
    DebugLog debugLog;

    BackgroundWorker worker = new BackgroundWorker();

    public Computer(Subject _subject, Options _arguments)
    {
        // tie reference to subject and add observer to observers list
        this.subject = _subject;
        subject.registerObserver(this);

        // initialize debug streams
        debugLog = new DebugLog();

        // initialize memory and registers (pass down subject to registers)
        memory = new Memory( subject, _arguments.memSize);
        registers = new Registers();

        // Initalize CPU and hook up references between CPU and RAM/Registers
        cpu = new CPU( memory, registers);

        // Configure BackgroundWorker
        this.worker.WorkerSupportsCancellation = true;
        this.worker.DoWork += this.worker_DoWork;
        this.worker.RunWorkerCompleted += this.worker_RunWorkerCompleted;


    }


    //-----------------------------------------------------Background thread helper methods -----------------------------------------//
    internal void runWorkerOnSeparateThread()
    {
        // start worker_DoWork
        worker.RunWorkerAsync();
    }
    
    /// FUNCTION: Runs on background thread
    ///             - Begin simulation of execution of a program starting at PC register.
    ///             - On this background thread do fetch-decode-execute 4x per second.
    ///             - notify form observer to update panels when
    ///                 - user hits stop/break or
    ///                 - cpu fetches 0
    private void worker_DoWork(object sender, DoWorkEventArgs e)
    {
        this.run();
    }

    ///  FUNTION: - Called by GUI to stop thread or called at the end of the program
    ///           - Call method worker_RunWorkerCompleted() to notify observers to update panels
    internal void cancelWorkerThread()
    {
        worker.CancelAsync(); // set CancellationPending flag       
    }

    // FUNTION: Runs on GUI thread, after worker_DoWork() returns. Tell GUI to update the panels
    private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        // Call notify on observers. Tell GUI to update panels
        subject.notifyObservers_AboutStopExecution();
    }

    // FUNCTION OVERRIDE IN OBSERVER: Empty method. This method is needed for observer pattern
    public void Notify_StopExecution()
    {

    }

    //FUNCTION OVERRIDE IN OBSERVER:: Empty method. This method is needed for observer pattern
    public void Notify_OneCycle()
    {
        
    }

    //FUNCTION OVERRIDE IN OBSERVER:: Empty method. This method is needed for observer pattern
    public void Notify_WriteCharToTerminal()
    {

    }

    //-----------------------------------------------------End Background thread helper methods -----------------------------------------//

    public string getMemChecksum(){
        return memory.ComputeCurrentMD5RAM();
    }

    /// FUNCTION: performs a fetch(), decodeLaS(), and executeLaS() until halted by user, 
    ///           zero word read, or done executing instructions
    /// RECEIVES: a Backgroundworker object to check if the task has been stopped by the user.
    /// RETURNS: 0 when run method is finished or reads a zero instruction
    public int run()
    {

        uint wordRead = 1;
        bool stopExecutingInstructs = false;

        // call these event until a instruction that contains a zero is read
        while ((wordRead != 0) && (!worker.CancellationPending) && (!stopExecutingInstructs) )
        {
            wordRead = cpu.fetch();
            cpu.decode();
            stopExecutingInstructs = cpu.execute();
        }

        // Cancel Thread to update panels if it's not canceled yet.
        worker.CancelAsync();

        return 0;
    }

    /// FUNCTION: performs one fect(), decodeLaS(), executeLaS()
    ///           updates step counter by one
    public void step()
    {
        // call these events only once
        cpu.fetch();
        cpu.decode();
        cpu.execute();

    }

    /****************************** Events Helper methods ************************/

    ///  HELPER FUCNTION: to loadSegmentsIntoRAM(). Converts a byte array to a struct
    ///  RECIEVES: a byte array
    ///  RETURNS: a struct
    public T ByteArrayToStructure<T>(byte[] bytes) where T : struct
    {
        GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        T stuff = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(),
            typeof(T));
        handle.Free();
        return stuff;
    }

    /// FUNCTION: Open ELF file, read elf header, read each program header entr (PHE) and store segment(s) to RAM
    ///           if succesful elf header read, then
    ///             - clear memory and registers
    ///             - set programCounter with elf header entry point
    ///             - load PHEs to RAM
    /// RETURNS:
    ///           returns -1 if failed to load segments into ram
    ///           returns 0 if success
    public int loadSegmentsIntoRAM(string fileName)
    {
        try
        { // try openning the file
            using (FileStream strm = new FileStream(fileName, FileMode.Open))
            {

                ELF elfHeader = new ELF();
                byte[] data = new byte[Marshal.SizeOf(elfHeader)];

                strm.Read(data, 0, data.Length); // Read ELF header data
                elfHeader = ByteArrayToStructure<ELF>(data); // Convert to struct

                //Console.WriteLine(@"This application displays the checksum of the RAM which contains the .exe's segment(s). To see a detailed debug logging information, open 'armmemoryLogFile.txt'. This file is in the executable's directory.");

                debugLog.WriteLineToLog("IMPORTANT: Debug logging information for developers:                         " + DateTime.Now);
                debugLog.WriteLineToLog("Loader: in loadSegmentsIntoRAM (): Opening " + fileName + "…");
                debugLog.WriteLineToLog("Loader: in loadSegmentsIntoRAM (): Program entry address: " + elfHeader.e_entry);
                debugLog.WriteLineToLog("Loader: in loadSegmentsIntoRAM (): Program header offset (location in file): " + elfHeader.e_phoff);
                debugLog.WriteLineToLog("Loader: in loadSegmentsIntoRAM (): Size of Program header entry: " + elfHeader.e_phentsize);
                debugLog.WriteLineToLog("Loader: in loadSegmentsIntoRAM (): Number of segments: " + elfHeader.e_phnum); //NumberelfHeader.e_entry.ToString("X4"));

                // clear memory, registers, nzcv flags
                memory.clearMemory();
                registers.clearRegisters();
                registers.clearNZCVFlags();
                    
                // update program counter r15
                registers.updateProgramCounter(elfHeader.e_entry);

                // update stack pointer r13
                registers.updateStackPointer(0x7000);


                // Read all program header entries and load segments to RAM
                uint nextElfHeaderOffset = elfHeader.e_phoff;
                int segmentNum = 0;
                for (int i = 0; i < elfHeader.e_phnum; i++, segmentNum++) //  Read all program headers
                {
                    // Read a program header
                    PHE headerTable = new PHE();
                    data = new byte[elfHeader.e_phentsize];
                    strm.Seek(nextElfHeaderOffset, SeekOrigin.Begin); // move position to start of next program header entry

                    strm.Read(data, 0, (int)elfHeader.e_phentsize); // Read ELF header data
                    headerTable = ByteArrayToStructure<PHE>(data);  // Convert to struct

                    // Load PHE to RAM
                    strm.Seek(headerTable.p_offset, SeekOrigin.Begin); // move position to start of segment
                    data = new byte[headerTable.p_filesz]; // resize data
                    strm.Read(data, 0, (int)headerTable.p_filesz); // Read segment to data

                    // load to registers
                    int succesfulLoad = memory.loadRAMfromDataArrayBytes(ref data, ref headerTable);
                    if (succesfulLoad == -1) // return if fail to load registers, mostlikely it doesn't fit in the RAM
                        return -1;

                    debugLog.WriteLineToLog("Loader: Segment " + segmentNum +
                                    " - vAddress = " + headerTable.p_vaddr +
                                    ", File Offset: " + headerTable.p_offset +
                                    ", Size: " + headerTable.p_filesz);
                    nextElfHeaderOffset += ((uint)elfHeader.e_phentsize);
                }

                string currentMD5RAM = memory.ComputeCurrentMD5RAM();
                debugLog.WriteLineToLog("Loader: in loadSegmentsIntoRAM (): Current RAM hex digest (RAM has MD5): " + currentMD5RAM);
                //Console.WriteLine("\nCurrent RAM hex digest: " + currentMD5RAM);

            } // using statement

        }
        catch (Exception e)
        { // Catch if file cannot be openned
            debugLog.WriteLineToLog("Loader: in loadSegmentsIntoRAM(): File cannot be opened: " + e.Message);
            //Console.WriteLine("For help try: armmemory.exe --help");
            //System.Threading.Thread.Sleep(3000);
            MessageBox.Show(e.Message, "Error Openning Executable File",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
            return -1;
        }

        return 0;
    } /************************ End of helper methods ***********************************/
    
    /************************ Helper methods for Form1 Class to Communicate with Registers and Memory Classes ******************/
    public void clearMemoryAndRegisters()
    {
        registers.clearRegisters();
        memory.clearMemory();
    }
    internal uint getProgramCounter() { return registers.getProgramCounter(); }
    internal void start() { throw new NotImplementedException(); }
    
    /* Trace Log Methods */
    internal bool isTraceLogEnabled() { return cpu.isTraceLogEnabled(); }
    public void resetTraceCounterToOne() { cpu.resetTraceCounterToOne(); }
    internal void turnOffTraceLog() { cpu.turnOffTraceLog(); }
    internal void turnOnTraceLog() { cpu.turnOnTraceLog(); }
    internal void traceLogFlush() { cpu.traceLogFlush(); }
    internal void traceLogClose() { cpu.traceLogClose(); }

    /* Debug Log Methods */
    internal void debugLogFlush() { debugLog.flush(); }
    internal void debugLogClose() { debugLog.close(); }

    /* Memory and Register related methods */
    internal uint[] getRegistersIntArray() { return registers.getRegistersIntArray(); }
    internal uint[] getControlFlagsIntArray() { return registers.getControlFlagsIntArray(); }
    internal string[] getRegisterNames()     { return registers.getRegisterNames(); }
    internal uint[] getFourWordsFromMemory(uint startAddress) { return memory.getFourWordsFromMemory(startAddress); }
    internal string[] getControlFlagNames()  { return registers.getControlFlagNames();}

    /* CPU list methods */
    internal List<string> getCPUInstructionList(){return cpu.getInstructionList();}
    internal List<string> getCPUInstructionAddressList() { return cpu.getInstructionAddressList(); }
    internal List<string> getCPUInstructionDisassembledList() { return cpu.getInstructionDisassembledList(); }
    internal void clearCPUInstructionAddressDisassembledLists(){cpu.clearInstAddrDisLists();}

    //internal void TestArmSimFormRef_OnWriteCharToTerminal()
    //{
    //    //ArmSimFormRef.WriteCharToTerminal("Hello World!");
    //    memory.ReadByte(0x00100001);
    //}

    internal uint getStackInstruction(uint address)
    {
        return memory.ReadWord(address);
    }

    // Getter functions to get items in CPU lists: instruction address, instruction, disassembled
    internal int getCPUDisListCount()
    {
       return cpu.getCPUDisListCount();
    }

    internal string getCPUInstructionAddressItem_At(int itemNumber)
    {
        return cpu.getCPUInstructionAddressItem_At(itemNumber);
    }

    internal string getCPUInstructionItem_At(int itemNumber)
    {
        return cpu.getCPUInstructionItem_At(itemNumber);
    }

    internal string getCPUInstructionDisassembledItem_At(int itemNumber)
    {
        return cpu.getCPUInstructionDisassembledItem_At(itemNumber);
    }

    internal string getCPUDisassembledCombinedString()
    {
        return cpu.getCPUDisassembledCombinedString();
    }

    internal void clearCPUDisassembledCombinedString()
    {
        cpu.clearCPUDisassembledCombinedString();
    }

    internal string getDisassembledLastInstructionExecuted()
    {
        return cpu.getDisassembledLastInstructionExecuted();
    }
}
