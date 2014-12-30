using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace armsim.Prototype
{
    class LoadAndStoreMul
    {
        private Memory memory;
        private Registers registers;

        private string instructionString;
        private uint instructAddress;
        private uint instruction;
        private bool P;
        private bool U;
        private bool B; // true = byte
        private bool W; // true = writeback
        private bool L; // true = load, false = store

        private uint Rn;
        private uint RnRegVal; // RnRegVal holds register actual value


        public LoadAndStoreMul(Memory _memory, Registers _registers, uint _currentInstruction, uint _currentInstAddress)
        {
            this.instructionString = "Not Found Yet";
            this.instruction = _currentInstruction;
            this.instructAddress = _currentInstAddress;

            // hook up registers and mememory object references
            this.registers = _registers;
            this.memory = _memory;
        }

        internal void decodeLaSMul()
        {
            // update field Rn and instructionString
            Rn = (instruction >> 16) & 0xF;

            // update fields: PUBWL
            P = memory.TestFlag(instructAddress, 24);
            U = memory.TestFlag(instructAddress, 23);
            B = memory.TestFlag(instructAddress, 22);
            W = memory.TestFlag(instructAddress, 21);
            L = memory.TestFlag(instructAddress, 20);
            updateInstructionStringWithInstructionName();

            // update field instructionString with Rn and register list
            instructionString += " " + registers.getRegisterName(Rn);
            if (W)
                instructionString += "!";

            instructionString += ", {";
            for (uint i = 0; i <= 15; i++)
                if (memory.TestFlag(instructAddress, i))
                    instructionString += registers.getRegisterName(i) + ", ";

            instructionString = instructionString.Remove(instructionString.Length - 2); //remove the last 2 chars (", ")
            instructionString += "}";

            // update field RnRegVal
            // if Rn is PC, then RnRegVal should be currentAddress + 8 bytes. else it it's the content of register Rn
            RnRegVal = registers.getRegNValue(Rn);
            if (Rn == 15)
                RnRegVal = instructAddress + 8;

            // update field unpredictable
            //if (Rn == 15 || registerList < 1) unpredictable = true;
        }

        private void updateInstructionStringWithInstructionName()
        {
            if (L) // L = 1 , then LOAD MULTIPLE or pop
                if (W && (Rn == 13) )
                    instructionString = "pop";  // W = true, pop with write back on Rn
                
                else if (W && !U) // U = 0, then go downwards
                    instructionString = "ldmia";
                
                else
                    instructionString = "ldm";  // W = false, pop with no write back on Rn

            else   // L = 0 , then STORE MULTIPLE or push
                if (W && (Rn == 13))
               
                    instructionString = "push"; // W = true, push with write back on Rn

                else if (W && U) // U = 1, then go upwards
                   instructionString = "stmia";
                
                else
                    instructionString = "stm";  // W = false, push with no write back on Rn

        }



        internal void executeLaSMul()
        {

            if (L)
                // L = 1 , then LOAD MULTIPLE or pop
                // W = true, pop with write back on Rn if W flag is set
                // Full Descending Stack
                LDMFD();

            else
                // STMIA
                // Empty Ascending Stack
                if (U)
                {
                    // if U = 1, then Empty ascending	STMEA (STMIA)	LDMEA (LDMDB)
                    // W = true, push with write back on Rn if W flag is set
                    STMIA();

                    // if U = 0, then Full descending	STMFD (STMDB)	LDMFD (LDMIA)
                }

                // Full Descending Stack
                else
                {
                    // L = 0 , then STORE MULTIPLE or push
                    // W = true, push with write back on Rn if W flag is set
                    STMFD();

                }

        }


        // FUNCTION: - stores multiple registers according to what registers are flagged true in fied instruction
        //           - write back into Rn register if W flag is set.
        //           - STM/STMIA/STMEA
        private void STMIA()
        {
            // start 4 bytes higher because R13 (SP) is pointing to full stack (we don't want to override 4 bytes)
            // point R13 to Empty Ascending stack
            //uint startOfStackAddr = registers.getstartOfStackAddress();
            uint Rn_val_tmp = RnRegVal;// +4;

            // LOAD MULTIPLE REGISTERS: start testing register 16 and load it (at Rn value) if reg flag is set
            for (uint i = 0; i <= 15; i++)
                if (memory.TestFlag(instructAddress, i)) // if reg flag is set
                {
                    memory.WriteWord(Rn_val_tmp, registers.getRegNValue(i));
                    Rn_val_tmp += 4; // R13 points to next word in stack (getting closer to higher addresses)
                }

            // WRITE BACK 
            // the final address into register Rn according to W flag
            if (W)
                registers.updateRegisterN(Rn, Rn_val_tmp);

        }



        // FUNCTION: - stores multiple registers according to what registers are flagged true in fied instruction
        //           - write back into Rn register if W flag is set.
        //           - STMDB/STMFD
        private void STMFD()
        {
            // start 4 bytes lower because R13 (SP) is pointing to full stack (we don't want to override 4 bytes)
            uint Rn_val_tmp = RnRegVal;

            // STORE MULTIPLE REGISTERS: start testing register 16 and store it (at Rn value) if reg flag is set
            for (int i = 15; i >= 0; i--)
                if (memory.TestFlag(instructAddress, (uint)i)) // if reg flag is set
                {
                    Rn_val_tmp -= 4; // R13 points to next empty slot (getting closer to lower addresses)

                    memory.WriteWord(Rn_val_tmp, registers.getRegNValue((uint)i)); // store reg at address pointed by Rn
                }

            // WRITE BACK 
            // the final address into register Rn according to W flag
            if (W)
                registers.updateRegisterN(Rn, Rn_val_tmp);

        }

        // FUNCTION: - load multiple registers according to what registers are flagged true in fied instruction
        //           - write back into Rn register if W flag is set.
        private void LDMFD()
        {
            //R13 (SP) is pointing to full stack (last word in stack)
            uint Rn_val_tmp = RnRegVal; 

            // LOAD MULTIPLE REGISTERS: start testing register 16 and load it (at Rn value) if reg flag is set
            for (uint i = 0; i <= 15; i++)
                if (memory.TestFlag(instructAddress, i)) // if reg flag is set
                {
                    registers.updateRegisterN(i, memory.ReadWord(Rn_val_tmp) );
                    Rn_val_tmp += 4; // R13 points to next word in stack (getting closer to higher addresses)
                }

            // WRITE BACK 
            // the final address into register Rn according to W flag
            if (W)
                registers.updateRegisterN(Rn, Rn_val_tmp);
        }

        internal string getInstructionString()
        {
            return instructionString;
        }
    }
}