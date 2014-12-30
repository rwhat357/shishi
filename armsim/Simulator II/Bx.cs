using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace armsim.Prototype
{
    class Bx
    {
        private Memory memory;
        private Registers registers;
        private string instructionString;
        private uint instructAddress;
        private uint instruction;
        private uint L; // indicates B or BL
        private uint targetAddress; // address to jump
        private uint Rm;
        private uint RmRegVal;

        public Bx(Memory _memory, Registers _registers, uint _currentInstruction, uint _currentInstAddress)
        {
            this.instructionString = "Not Found Yet";
            this.instruction = _currentInstruction;
            this.instructAddress = _currentInstAddress;

            // hook up registers and mememory object references
            this.registers = _registers;
            this.memory = _memory;
        }

        // BX (Branch and Exchange) branches to an address, with an optional switch to Thumb state.
        internal void decodeBx()
        {
            // update fields: Rm and RmRegVal 
            Rm = instruction & 0xf;
            RmRegVal = registers.getRegNValue(Rm);

            instructionString = "bx " + registers.getRegisterName(Rm);
        }

        // BX (Branch and Exchange) branches to an address, with an optional switch to Thumb state.
        // Return from a subroutine: MOV pc, r14
        internal void executeBx()
        {
            registers.updateRegisterN(15, RmRegVal);
        }

        internal string getInstructionString()
        {
            return instructionString;
        }
    }
}