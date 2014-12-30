using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace armsim
{
    public class Mul
    {
        private Memory memory;
        private Registers registers;

        private string instructionString;
        private uint S;

        // Rm, Rd, and Rs hold the register numbers
        private uint Rm;      
        private uint Rd;
        private uint Rs;

        private uint instruction;
        private uint instructAddress;

        public Mul(Memory _memory, Registers _registers, uint _currentInstruction, uint _currentInstAddress)
        {
            this.instructionString = "mul";
            this.instruction = _currentInstruction;
            this.instructAddress = _currentInstAddress;

            // hook up registers and mememory object references
            this.registers = _registers;
            this.memory = _memory;

        }

        public void decodeMul(){
            
            // get Rd
            Rd = (instruction >> 16) & 0xf;

            // get Rs
            Rs = (instruction >> 8) & 0xf;

            // get Rm
            Rm = instruction & 0xf;

        }

        public void executeMul()
        {
            // get the values from the registers
            uint RdVal = registers.getRegNValue(Rd);
            uint RsVal = registers.getRegNValue(Rs);
            uint RmVal = registers.getRegNValue(Rm);


            // do operation requested
            uint RmVal_x_RsVal = (uint)(RmVal * RsVal); //cast to 32-bit if overflow

            // move new value into into a register
            registers.updateRegisterN(Rd, RmVal_x_RsVal);


            // Register Shifted Register
            /* TODO: not implemented for simI
            if (bit25 == false && bit4 == true)  
                registers.updateRegisterN(Rd, immediate.getRotatedImmediate());
             */
        }


        internal string getInstructionString()
        {
            string RdRegName = registers.getRegisterName(Rd);
            string RsRegName = registers.getRegisterName(Rs);
            string RmRegName = registers.getRegisterName(Rm);

            instructionString += " " + RdRegName + ", " + RmRegName + ", " + RsRegName;

            return instructionString;
        }
    }
}