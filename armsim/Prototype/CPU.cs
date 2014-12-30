using System;
using System.Collections.Generic;
using System.Diagnostics; // Trace() and Debug()
using armsim; // my custom namespace
using System.Linq;
using armsim.Prototype;
using System.Threading;

namespace armsim
{

    class CPU //: Observer
    {
        // these list keep track of all the instructions that get executed
        List<string> instLst = new List<string>(); // instruction list
        List<string> instAddressLst = new List<string>(); // instruction address list
        List<string> instDisLst = new List<string>(); // instruction disassembled list

        // Objects that initially are null. They are created when needed to execute and decode
        // an instruction
        DataProcessing data_processing_instruct;
        LoadAndStore load_store_instruct;
        LoadAndStoreMul load_store_mul_instruct;
        Branch branch_instruct;
        Mul mul_instruct;
        Bx bx_instruct;

        Memory memory;
        Registers registers;
        TraceLog traceLog;
        Subject subject;

        string disassembledCombinedString; // holds all the disassembled text
        string lastDisString; // holds last disassembled instruction 

        uint currentInstructionType;
        uint currentInstAddress;
        uint currentInstruction;

        //bool stopExec; // if true, stops decode/execute instructions. It's set to tru by execute() when encountering SWI.

        public CPU( Memory mem, Registers reg)
        {

            // tie reference to subject and add observer to list
            //this.subject = subj;
            //this.subject.registerObserver(this);

            // hook up references between CPU and RAM/Registers
            this.memory = mem;
            this.registers = reg;
            
            // initialize fields
            this.currentInstAddress = 0;
            this.currentInstruction = 0;
            this.currentInstructionType = 0;
            this.disassembledCombinedString = "";
            this.lastDisString = "";

            // initialize instruction types to null
            this.data_processing_instruct = null;
            this.load_store_instruct = null;
            this.branch_instruct = null;
            this.mul_instruct = null;


            // initialize tracelog
            traceLog = new TraceLog();
        }

        // FUNCTION: Fetch the instruction and its address
        //           update fields <currentInstruction> and <currentInstAddress>
        // RETURNS:  an integer to be tested. i.e. return zero if we fetch a zero (this means that we are done        
        public uint fetch()
        {
            uint programCounter = registers.getProgramCounter();
            uint word = memory.ReadWord(programCounter);

            this.currentInstruction = word;
            this.currentInstAddress = programCounter;

            this.registers.incrementProgramCounterInAWordSize();
            return word;

        }

        /// FUNCTION: 
        ///          - create an object and decode field currentInstruction according to its type: DataProcessing, Load/Store, or Branch
        ///          - store the decoded details in one of these field objects: data_processing_instruct, load_store_instruct, branch_instruct.
        ///            the unused 2 objects of these 3 objects have value null
        public void decode()
        {

            /*************************** SPECIAL CASES ****************************/
            // SWI CASE
            if (Extras.isSWIInstruction(currentInstruction) )
                return;

            // MUL CASE
            if (Extras.isMulInstruction(currentInstruction))
            {
                mul_instruct = new Mul(memory, registers, currentInstruction, currentInstAddress);
                mul_instruct.decodeMul();
                return;
            }

            // BX CASE
            if (Extras.isBX(currentInstruction))
            {
                bx_instruct = new Bx(memory, registers, currentInstruction, currentInstAddress);
                bx_instruct.decodeBx();
                return;
            }


            /*************************** NORMAL CASES ******************************/
            // get instruction type
            currentInstructionType = Extras.getNormalInstructionType(currentInstruction);


            // NORMAL CASES: create an instruction according to its type 
            // and decode the instruction
            switch (currentInstructionType)
            { 
                case 0: // if Data Processing (00x)
                case 1: // if Data Processing (00x)
                    data_processing_instruct = new DataProcessing(memory, registers, currentInstruction, currentInstAddress);
                    data_processing_instruct.decodeDP();
                    break;

                case 2: // if Load/Store (01x)
                case 3: // if Load/Store (01x)
                    load_store_instruct = new LoadAndStore(memory, registers, currentInstruction, currentInstAddress);
                    load_store_instruct.decodeLaS();
                    break;

                case 4: // if Load/Store (100) Load/Store Multiple FD variant
                    load_store_mul_instruct = new LoadAndStoreMul(memory, registers, currentInstruction, currentInstAddress);
                    load_store_mul_instruct.decodeLaSMul();
                    break;

                case 5: // if Branch (101)
                    branch_instruct = new Branch(memory, registers, currentInstruction, currentInstAddress);
                    branch_instruct.decodeB();
                    break;

                default:
                    instDisLst.Add("Special Instruction");
                    Debug.WriteLine("ERROR in CPU.execute(): Type is Special or Non-valid instruction.");
                    Debug.WriteLine("ERROR in CPU.decode(): Type is Special or Non-valid instruction.");
                    break;
            }
        }

        /// FUNCTION: 
        ///         - execute current instruction according to its type: DataProcessing, Load/Store, or Branch
        ///         - uses field currentInstructionType to determine which instruction to run
        ///         - adds a string into fields instAddressLst, instLst, and instDisLst
        /// RETURNS:
        ///         - FALSE if the instruction executed is not SWI
        ///         - TRUE if instruction executed was SWI. Caller must check to stop executing the rest of the instructions.
        public bool execute()
        {

            // add address, instruction, and disassembled instruction strings to each list
            string addr = "0x" + currentInstAddress.ToString("X").PadLeft(8, '0');
            string inst = currentInstruction.ToString("x").PadLeft(8, '0');
            //instAddressLst.Add(addr);
            //instLst.Add(inst);

            // prepare string
            disassembledCombinedString = disassembledCombinedString + "\r\n" + addr + "\t" + inst + "\t";
            lastDisString = disassembledCombinedString;

            // will be used to collect strings to later concatenate with field string disassembledCombinedString
            string tempStr = "";

            // INJECT A NOP into execute if instruction is a NOP
            if (Extras.isNOPInstruction(currentInstruction, registers.getNFlag(), registers.getZFlag(), registers.getCFlag(), registers.getVFlag()))
            {
                writeInfoToTraceLog();
                //instDisLst.Add("NOP");
                disassembledCombinedString += "NOP";
                lastDisString += "NOP";
                return false;
            }



            /************************SPECIAL CASES ****************************/
            // SWI
            if (Extras.isSWIInstruction(currentInstruction))
            {
                //Debug.WriteLine("SWI in CPU.decode(). End of instructions");
                uint swi_imm = currentInstruction & 0x00ffffff;

                // add swi immediate to disassembled list
                tempStr = "svc 0x" + swi_imm.ToString("x").PadLeft(8, '0');
                //instDisLst.Add(tempStr);
                disassembledCombinedString += tempStr;
                lastDisString += tempStr;

                writeInfoToTraceLog();
                return true; // true to stop executing more instructions
            }

            // MUL
            if (Extras.isMulInstruction(currentInstruction))
            {
                mul_instruct.executeMul();

                tempStr = mul_instruct.getInstructionString();
                //instDisLst.Add(tempStr);
                disassembledCombinedString += tempStr;
                lastDisString += tempStr;

                writeInfoToTraceLog();
                return false; // false to keep excuting more instructions
            }

            // BX CASE
            if (Extras.isBX(currentInstruction))
            {
                bx_instruct.executeBx();

                tempStr = bx_instruct.getInstructionString();
                //instDisLst.Add(tempStr);
                disassembledCombinedString += tempStr;
                lastDisString += tempStr;

                writeInfoToTraceLog();
                return false; // false to keep excuting more instructions
            }

            /************************NORMAL CASES ****************************/
            // execute the instruction according to its type 
            switch (currentInstructionType)
            {

                case 0: // if Data Processing (00x)
                case 1: // if Data Processing (00x)
                    data_processing_instruct.executeDP();

                    tempStr = data_processing_instruct.getInstructionString();
                    //instDisLst.Add(tempStr);
                    disassembledCombinedString += tempStr;
                    lastDisString += tempStr;
                    break; 

                case 2: // if Load/Store (01x)
                case 3: // if Load/Store (01x)
                    load_store_instruct.executeLaS();

                    tempStr = load_store_instruct.getInstructionString();
                    //instDisLst.Add(tempStr);
                    disassembledCombinedString += tempStr;
                    lastDisString += tempStr;
                    break;

                case 4: // if Load/Store (100) Load/Store Multiple FD variant
                    load_store_mul_instruct.executeLaSMul();

                    tempStr = load_store_mul_instruct.getInstructionString();
                    //instDisLst.Add(tempStr);
                    disassembledCombinedString += tempStr;
                    lastDisString += tempStr;
                    break;

                case 5: // if Branch (101)
                    branch_instruct.executeB();

                    tempStr = branch_instruct.getInstructionString();
                    //instDisLst.Add(tempStr);
                    disassembledCombinedString += tempStr;
                    lastDisString += tempStr;
                    break;

                // TODO: Check special cases
                default:
                    Debug.WriteLine("ERROR in CPU.execute(): Type is Special or Non-valid instruction.");
                    break;
            } // end switch

            writeInfoToTraceLog();

            //Thread.Sleep(2000);

            return false;
        }

        /* List getter methods */
        internal List<string> getInstructionList() { return this.instLst; }
        internal List<string> getInstructionAddressList(){return this.instAddressLst;}
        internal List<string> getInstructionDisassembledList(){return this.instDisLst;}

        /* Clear all three lists */
        internal void clearInstAddrDisLists()
        {
            instLst.Clear();
            instAddressLst.Clear();
            instDisLst.Clear();
        }

        // HELPER FUNCTION TO TEST DECODE EXECUTE METHODS
        // RETURNS: last items added to all three lists in a string format
        // SAMPLE FORMAT: 0x00001014: e3a004fe: mov r0, #-33554432
        internal string getTopInstAddrDisString()
        {
            int rowNum = instLst.Count() - 1;

            string addr = instAddressLst.ElementAt<string>(rowNum);
            string inst = instLst.ElementAt<string>(rowNum);
            string dis = instDisLst.ElementAt<string>(rowNum);
            return (addr + ": " + inst + ": " + dis);

        }

        // HELPER FUNCTION TO TEST DECODE EXECUTE METHODS
        // RETURNS: last items added to all three lists in a string format
        // SAMPLE FORMAT: 101c: e35004ff cmp r0, #-16777216
        internal string getTopInstAddrDisStringNotepadFormat()
        {
            int rowNum = instLst.Count() - 1;

            string addr = instAddressLst.ElementAt<string>(rowNum);
            addr = addr.Remove(0, 6); // Removes the first 6 characters (removes 0x0000)
            addr = addr.ToLower();

            string inst = instLst.ElementAt<string>(rowNum);
            string dis = instDisLst.ElementAt<string>(rowNum);
            return (addr + ": " + inst + " " + dis);

        }

        internal uint getCPUCurrentAddr(){return currentInstAddress;}


        /// Print a 3 line info to trace log file in this format:
        /// 
        /// step_number program_counter checksum nzcf r0 r1 r2 r3
        /// r4 r5 r6 r7 r8 r9
        /// r10sl r11fp r12 r13 r14
        /// 
        /// Example:
        /// 
        /// 000001 41414141 d41d8cd98f00b204e9800998ecf8427e 0101 0=01234567 1=01234567 2=01234567 3=01234567
        /// 4=01234567  5=01234567  6=01234567  7=01234567  8=01234567 9=01234567
        /// 10=01234567 11=01234567 12=01234567 13=01234567 14=01234567
        /// Tracelog for SIM II
        private void writeInfoToTraceLog()
        {
            if (traceLog.isEnabled() == false)
                return;

            // getting the needed values
            int traceCounter = traceLog.getTraceCounter();
            uint programCounter = currentInstAddress; // program_counter is the value of the program counter at the time the fetch began

            string machine_state = "[sys]";

            uint[] controlFlagsIntArray = registers.getControlFlagsIntArray();
            uint[] registersIntArray = registers.getRegistersIntArray();

            // converting values to string formatted to print them later
            string strTC = traceCounter.ToString().PadLeft(6, '0');
            string strPC = programCounter.ToString("X").PadLeft(8, '0');  // gives you hex

            // printing values to trace log by lines
            traceLog.WriteLineToLog(strTC + " " + strPC + " " + machine_state + " " +
                                    controlFlagsIntArray[0] + controlFlagsIntArray[1] + controlFlagsIntArray[2] + controlFlagsIntArray[3] +

                                     " 0=" + registersIntArray[0].ToString("X").PadLeft(8, '0') +
                                     " 1=" + registersIntArray[1].ToString("X").PadLeft(8, '0') +
                                     " 2=" + registersIntArray[2].ToString("X").PadLeft(8, '0') +
                                     " 3=" + registersIntArray[3].ToString("X").PadLeft(8, '0') );

            traceLog.WriteLineToLog(
                                    "\t4=" + registersIntArray[4].ToString("X").PadLeft(8, '0') +
                                    "  " + "5=" + registersIntArray[5].ToString("X").PadLeft(8, '0') +
                                    "  " + "6=" + registersIntArray[6].ToString("X").PadLeft(8, '0') +
                                    "  " + "7=" + registersIntArray[7].ToString("X").PadLeft(8, '0') +
                                    "  " + "8=" + registersIntArray[8].ToString("X").PadLeft(8, '0') +
                                    " " + "9=" + registersIntArray[9].ToString("X").PadLeft(8, '0'));

            traceLog.WriteLineToLog(
                                     "\t10=" + registersIntArray[10].ToString("X").PadLeft(8, '0') +
                                     " 11=" + registersIntArray[11].ToString("X").PadLeft(8, '0') +
                                     " 12=" + registersIntArray[12].ToString("X").PadLeft(8, '0') +
                                     " 13=" + registersIntArray[13].ToString("X").PadLeft(8, '0') +
                                     " 14=" + registersIntArray[14].ToString("X").PadLeft(8, '0'));

            traceLog.updateStepCounterByOne();
        }

        // Trace Log for SIM I
        private void writeInfoToTraceLogForSimI()
        {
            // getting the needed values
            int traceCounter = traceLog.getTraceCounter();
            uint programCounter = registers.getProgramCounter() - 4; // program_counter is the value of the program counter at the time the fetch began

            string checksum = memory.ComputeCurrentMD5RAM();

            uint[] controlFlagsIntArray = registers.getControlFlagsIntArray();
            uint[] registersIntArray = registers.getRegistersIntArray();

            // converting values to string formatted to print them later
            string strTC = traceCounter.ToString().PadLeft(6, '0');
            string strPC = programCounter.ToString("X").PadLeft(8, '0');  // gives you hex

            // printing values to trace log by lines
            traceLog.WriteLineToLog(strTC + " " + strPC + " " + checksum + " " +
                                    controlFlagsIntArray[0] + controlFlagsIntArray[1] + controlFlagsIntArray[2] + controlFlagsIntArray[3] +

                                    "   0=" + registersIntArray[0].ToString("X").PadLeft(8, '0') +
                                     "  1=" + registersIntArray[1].ToString("X").PadLeft(8, '0') +
                                     "  2=" + registersIntArray[2].ToString("X").PadLeft(8, '0') +
                                     "  3=" + registersIntArray[3].ToString("X").PadLeft(8, '0') + " ");

            traceLog.WriteLineToLog(
                                    "        4=" + registersIntArray[4].ToString("X").PadLeft(8, '0') +
                                    "  " + "5=" + registersIntArray[5].ToString("X").PadLeft(8, '0') +
                                    "  " + "6=" + registersIntArray[6].ToString("X").PadLeft(8, '0') +
                                    "  " + "7=" + registersIntArray[7].ToString("X").PadLeft(8, '0') +
                                    "  " + "8=" + registersIntArray[8].ToString("X").PadLeft(8, '0') +
                                    "  " + "9=" + registersIntArray[9].ToString("X").PadLeft(8, '0') + " ");

            traceLog.WriteLineToLog(
                               "       10=" + registersIntArray[10].ToString("X").PadLeft(8, '0') +
                                     " 11=" + registersIntArray[11].ToString("X").PadLeft(8, '0') +
                                     " 12=" + registersIntArray[12].ToString("X").PadLeft(8, '0') +
                                     " 13=" + registersIntArray[13].ToString("X").PadLeft(8, '0') +
                                     " 14=" + registersIntArray[14].ToString("X").PadLeft(8, '0') + " ");
        }


        /* Trace Log Methods */
        internal bool isTraceLogEnabled() { return traceLog.isEnabled(); }
        internal void resetTraceCounterToOne() { traceLog.resetTraceCounterToOne(); }
        internal void turnOffTraceLog() { traceLog.turnOffTraceLog(); }
        internal void turnOnTraceLog() { traceLog.turnOnTraceLog(); }
        internal void traceLogFlush() { traceLog.flush(); }
        internal void traceLogClose() { traceLog.close(); }

        // Getter functions to get items in CPU lists: instruction address, instruction, disassembled
        internal int getCPUDisListCount()
        {
            return instDisLst.Count();
        }

        internal string getCPUInstructionAddressItem_At(int itemNumber)
        {
            return instAddressLst.ElementAt(itemNumber);
        }

        internal string getCPUInstructionItem_At(int itemNumber)
        { 
            return instLst.ElementAt(itemNumber); 
        }

        internal string getCPUInstructionDisassembledItem_At(int itemNumber)
        {
            return instDisLst.ElementAt(itemNumber);
        }

        internal string getCPUDisassembledCombinedString()
        {
            return disassembledCombinedString;
        }

        internal void clearCPUDisassembledCombinedString()
        {
            disassembledCombinedString = "";
        }

        internal string getDisassembledLastInstructionExecuted()
        {
            return lastDisString;
        }
    }


}