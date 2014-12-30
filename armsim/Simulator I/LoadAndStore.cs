using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace armsim
{
    class LoadAndStore
    {
        private Memory memory;
        private Registers registers;

        private RegAndImmShReg imm_sh_reg;    // operand2 type

        private string instructionString;
        private uint instructAddress;
        private uint instruction;
        private bool P;
        private bool U;
        private bool B;
        private bool W;
        private bool L;// true = load, false = store

        private uint Rn;
        private uint RnRegVal; // RnRegVal holds register actual value
        private uint Rd;
        private bool bit_25; // Indicates whether flag is set to identify the kind of offset. 
                             // False indicates immediate. True indicates immediate shifted regiter

        // offset fields
        private uint _12bitImmediate;
        private uint effectiveAddress;


        public LoadAndStore(Memory _memory, Registers _registers, uint _currentInstruction, uint _currentInstAddress)
        {
            this.instructionString = "Not Found Yet";
            this.instruction = _currentInstruction;
            this.instructAddress = _currentInstAddress;

            // hook up registers and mememory object references
            this.registers = _registers;
            this.memory = _memory;
        }

        public void executeLaS()
        {
            if (L) // 1 == load
                LDR();
            else   // 0 == store
                STR();

        }

        // FUNCTION: deter
        public void decodeLaS()
        {
            // check bit 25 on instruction to determine if it this instruction has
            // an offset: immediate or immediate shifted register
            bit_25 = memory.TestFlag(instructAddress, 25);

            // update fields: PUBWL
            P = memory.TestFlag(instructAddress, 24);
            U = memory.TestFlag(instructAddress, 23);
            B = memory.TestFlag(instructAddress, 22);
            W = memory.TestFlag(instructAddress, 21);
            L = memory.TestFlag(instructAddress, 20);
            updateInstructionStringWithInstructionName();

            // update field Rn 
            Rn = (instruction >> 16) & 0xf;

            // update field RnRegVal
            // if Rn is PC, then RnRegVal should be currentAddress + 8 bytes. else it it's the content of register Rn
            if (Rn == 15)
                RnRegVal = instructAddress + 8;
            else
                RnRegVal = registers.getRegNValue(Rn);

            // update field Rd
            Rd = (instruction >> 12) & 0xf;

            if (bit_25 == false) // OFFSET IS IMMEDIATE. Decode 12 bit op2 immediate
            {
                // update field _12bitImmediate
                _12bitImmediate = instruction & 0xfff;

                if (U) // Immediate is positive
                    effectiveAddress = RnRegVal + _12bitImmediate;
                else // Immediate is negative. TODO: check for negative numbers in unsigned subtraction
                    effectiveAddress = RnRegVal - _12bitImmediate;
            }
            else // OFFSET REGISTER SHIFTED REGISTER. Decode 12 bit  op2 register shifted register
            {
                imm_sh_reg = new RegAndImmShReg(memory, registers, instruction, instructAddress);
                imm_sh_reg.decode_RegAndImmShReg();
                imm_sh_reg.execute_RegAndImmShReg(); // calculates RmRegVal field in object by doing shifting bits

                if (U) // Immediate is positive
                    effectiveAddress = RnRegVal + imm_sh_reg.getRmRegVal();
                else // Immediate is negative. TODO: check for negative numbers in unsigned subtraction
                    effectiveAddress = RnRegVal - imm_sh_reg.getRmRegVal();
            }

        }

        private void updateInstructionStringWithInstructionName()
        {
            if (L) // L = 1 , then load
                if (B)
                    instructionString = "ldrb"; // B = true, then load byte
                else
                    instructionString = "ldr";  // B = false, then load word
            else // L = 0 , then store
                if (B)
                    instructionString = "strb";
                else
                    instructionString = "str";
        }

        internal string getInstructionString() { return instructionString;}
        
        public void LDR()
        {

                // load a byte or a word
                // B = true, then load byte
                // B = false, then load word
                if (B)
                    registers.updateRegisterN(Rd, memory.ReadByte(effectiveAddress));
                else
                    registers.updateRegisterN(Rd, memory.ReadWord(effectiveAddress));


                // update instructionString as immediate or immediate shifted register
                // OFFSET IS IMMEDIATE bit 25 = 0
                // OFFSET IS IMMEDIATE SHIFTED REGISTER bit 25 = 1
                if (!bit_25)
                    updateInstructionStringWithImmediate();
                else
                    updateInstructionStringWithImm_sh_reg();
        }

        public void STR()
        {
            // store a byte or a word
            // B = true, then store byte
            // B = false, then store word
            if (B) 
                memory.WriteByte(effectiveAddress, (byte)registers.getRegNValue(Rd));
            else   
                memory.WriteWord(effectiveAddress, registers.getRegNValue(Rd));


            // update instructionString as immediate or immediate shifted register
            // OFFSET IS IMMEDIATE bit 25 = 0
            // OFFSET IS IMMEDIATE SHIFTED REGISTER bit 25 = 1
            if (!bit_25) 
                updateInstructionStringWithImmediate();
            else         
                updateInstructionStringWithImm_sh_reg();

        }

        private void updateInstructionStringWithImm_sh_reg()
        {

            // get offset string
            // add base register
            string offsetStr = "[" + registers.getRegisterName(Rn) + ", ";


            // if op2 is register only
            if (imm_sh_reg.getShiftNum() == 0)
            {

                // add a minus (-) sign if immediate is negative
                if (!U)
                    offsetStr += "-";

                offsetStr += registers.getRegisterName(imm_sh_reg.getRm());
            }
            else // if op2 is immediate shifted register
            {
                offsetStr += imm_sh_reg.getOp2String();
            }

            offsetStr += "]";

            // update whole instruction string
            instructionString += " " + registers.getRegisterName(Rd) + ", " + offsetStr;
        }

        private void updateInstructionStringWithImmediate()
        {

            // get offset string
            // add base register
            string offsetStr = "[" + registers.getRegisterName(Rn);

            // add immediate if it is not zero
            if (_12bitImmediate != 0)
            {
                offsetStr += ", #";

                // add a minus (-) sign if immediate is negative
                if (!U)
                    offsetStr += "-";

                offsetStr += _12bitImmediate;
            }

            offsetStr += "]";


            // update whole instruction string
            instructionString += " " + registers.getRegisterName(Rd) + ", " + offsetStr;
        }

        public void STM()
        {
            throw new System.NotImplementedException();
        }

        public void LDM()
        {
            throw new System.NotImplementedException();
        }
        public void LDRB()
        {
            throw new System.NotImplementedException();
        }

        public void STRB()
        {
            throw new System.NotImplementedException();
        }
    }
}