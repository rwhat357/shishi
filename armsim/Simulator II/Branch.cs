using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace armsim
{
    class Branch
    {
        private Memory memory;
        private Registers registers;
        private string instructionString;
        private uint instructAddress;
        private uint instruction;
        private uint L; // indicates B or BL
        private uint targetAddress; // address to jump

        public Branch(Memory _memory, Registers _registers, uint _currentInstruction, uint _currentInstAddress)
        {
            this.instructionString = "Not Found Yet";
            this.instruction = _currentInstruction;
            this.instructAddress = _currentInstAddress;

            // hook up registers and mememory object references
            this.registers = _registers;
            this.memory = _memory;
        }

        /// FUNCTION DECODE FOR B AND BL
        /// B (Branch) and BL (Branch and Link) cause a branch to a target address, 
        /// and provide both conditional and unconditional changes to program flow.
        /// BL also stores a return address in the link register, R14 (also known as LR).
        public void decodeB()
        {
            // get field imm32
            uint imm24 = instruction & 0xFFFFFF;
            int imm32 = signExtend32(26, imm24 << 2);

            // get tagerAddress
            uint PC = registers.getProgramCounter();
            targetAddress = (uint)( (int)PC + imm32 + 4); // -4 because we want the current address + the offset
            //targetAddress = (uint)((int)instructAddress + imm32);

            // get L and update instructionString
            L = (instruction >> 24) & 0x1;
            instructionString = "b";
            if (L == 1)
                instructionString += "l";

            string condSuffix = Extras.getConditionalInstructionSuffix(instruction, registers.getNFlag(), registers.getZFlag(), registers.getCFlag(), registers.getVFlag());
            instructionString += condSuffix + " 0x" + targetAddress.ToString("x");
        }

        // HELPER FUNCTION FOR decodeB
        int signExtend32(int bitsPresent, uint val)
        {
            int temp = (int)val;
            int shift = 32 - bitsPresent;
            temp <<= (shift);
            return temp >> shift;
        }

        /// FUNCTION DECODE FOR B AND BL
        /// B (Branch) and BL (Branch and Link) cause a branch to a target address, 
        /// and provide both conditional and unconditional changes to program flow.
        /// BL also stores a return address in the link register, R14 (also known as LR).
        public void executeB()
        {
            // B, BL – Branch with Link
            //      R14 := address of next instruction, PC := <address>
            if (L == 1)
                registers.updateRegisterN(14, registers.getRegNValue(15)); // save next address in link reg

            // update program counter with new address
            registers.updateRegisterN(15, targetAddress);

        }

        internal string getInstructionString()
        {
            return instructionString;
        }
    }
}