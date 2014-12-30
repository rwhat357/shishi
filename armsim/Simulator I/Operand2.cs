using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace armsim
{

    // This file contains the 3 classes to decode and execute
    // the 3 different data processing operand2 instructions
    class Immediate
    {
        private Memory memory;
        private Registers registers;
        private uint instruction;
        private uint instructAddress;
        private uint rotNum; // #rot
        private uint _8BitImmediate; // 8-bit _8BitImmediate that will be rotated in a 32 bit word if <rotNum> is not zero
        private uint rotatedImmediate; // 8-bit immediate that is rotated right in a 32 bit by (rotNum * 2)

        public Immediate(Memory _memory, Registers _registers, uint _currentInstruction, uint _currentInstAddress)
        {
            // hook up registers and mememory object references
            this.registers = _registers;
            this.memory = _memory;

            this.instruction = _currentInstruction;
            this.instructAddress = _currentInstAddress;
            this.rotNum = 0;
            this._8BitImmediate = 0;
            this.rotatedImmediate = 0;
        }

        public uint getRotatedImmediate() { return rotatedImmediate; }

        // FUNCTION: 
        //      -Using instruction field update fields: rotNum, _8BitImmediate, rotatedImmediate
        public void decode_Imm()
        {
            // update field rotNum
            this.rotNum = (instruction >> 8) & 0xf;

            // update field _8BitImmediate
            this._8BitImmediate = instruction & 0xff;

            // update field rotatedImmediate
            this.rotatedImmediate = this._8BitImmediate;
            this.rotatedImmediate = BarrelShifter.ror(this.rotatedImmediate, rotNum * 2); // rotate 8bit immediate in a 32 bit address

        }

        // RETURNS: the immediate decimal and the immediate with in hex format
        internal string getImmString()
        {
            int rotatedImmediateInt = (int)rotatedImmediate;
            return ("#" + rotatedImmediateInt);// + "\t" + hexImm);
        }
    }

    class RegAndImmShReg
    {
        private Memory memory;
        private Registers registers;
        private string shiftName;
        private string op2String;

        private uint instruction;
        private uint instructAddress;

        private uint shiftTypeUInt; // contains the value of a register shifted by an immediate
        private uint shiftNum;

        private uint Rm;        // Rm holds the register number
        private uint RmRegVal;  // RmRegVal holds register actual value

        public RegAndImmShReg(Memory _memory, Registers _registers, uint _currentInstruction, uint _currentInstAddress)
        {
            this.shiftName = "Not Known Yet";
            this.op2String = "Not Known Yet";
            this.instruction = _currentInstruction;
            this.instructAddress = _currentInstAddress;

            // hook up registers and mememory object references
            this.registers = _registers;
            this.memory = _memory;
        }

        // HELPER FUCNTION: - to decode_Imm()
        //                  - decodes the shift name from the shift type
        // RETURNS:         -returns the shift name string
        //                  -returns "Invalid Shift Type" if it is an invalid type
        public string getShiftNameString(uint _shiftTypeUInt, uint _shiftNum)
        {
            // return string according to shift type bits
            //                          bits:  6  5
            switch (_shiftTypeUInt)
            {
                case 0:
                    return "lsl";           // 0  0
                case 1:
                    return "lsr";           // 0  1
                case 2:
                    return "asr";           // 1  0
                case 3:
                    if (_shiftNum == 0)
                        return "rrx";       // 1  1
                    else
                        return "ror";       // 1  1
                default:
                    Debug.WriteLine("ERROR in file 'Operand2' RegAndImmShReg.getShiftNameString(): type is out of range.");
                    return "Invalid Shift Type";
            }
        }

        // FUNCTION: 
        //      -Using instruction field update fields:  shiftNum, shiftTypeUInt,shiftName, Rm
        //      -decodes operand2 as Register with Immediate Shift
        public void decode_RegAndImmShReg()
        {

            // update field shiftNum
            shiftNum = (instruction >> 7) & 0x1f;

            // update field shiftTypeUInt
            shiftTypeUInt = (instruction >> 5) & 0x3;

            // update field shiftName
            shiftName = getShiftNameString(shiftTypeUInt, shiftNum);

            // update field Rm
            Rm = instruction & 0xf;

            // update field RnmRegVal
            // if Rm is PC, then RnRegVal should be currentAddress + 8 bytes. else it it's the content of register Rm
            if (Rm == 15)
                RmRegVal = instructAddress + 8;
            else
                RmRegVal = registers.getRegNValue(Rm);


        }


        // FUNCTION:- execute RegAndImmShReg operand2
        //          - update field RmRegVal which contains the Rm register actual content value
        public void execute_RegAndImmShReg()
        {


            // shift XReg by shiftNum accoding to shift type bits.
            switch (shiftTypeUInt)                                // bits:  6  5
            {
                case 0:
                    RmRegVal = BarrelShifter.lsl(RmRegVal, shiftNum);            // 0  0 = LSL
                    break;
                case 1:
                    RmRegVal = BarrelShifter.lsr(RmRegVal, shiftNum);            // 0  1 = LSR
                    break;
                case 2:
                    RmRegVal = BarrelShifter.asr(RmRegVal, shiftNum);            // 1  0 = ASR
                    break;
                case 3:
                    /* if (_shiftNum == 0) TODO: this no supported for sim I
                         XReg = BarrelShifter.lsl(XReg, this.shiftNum);           // 1  1 = RRX
                     else*/
                    RmRegVal = BarrelShifter.ror(RmRegVal, shiftNum);        // 1  1 = ROR
                    break;
                default:
                    Debug.WriteLine("ERROR in file 'Operand2' RegAndImmShReg.execute_Imm(): type is out of range.");
                    break;
            }

            //this.shiftedRegUInt = XReg;
            // write back the copy into the register specified by Rm only if write back specified
            // this.registers.updateRegisterN(this.Rm, XReg);
        }


        internal uint getRm()
        {
            return this.Rm;
        }

        // RETURNS: the operand2 in string format
        internal string getOp2String()
        {
            string RmRegName = registers.getRegisterName(Rm);

            if (shiftNum == 0) // There is no shift. So it was register shift register
                this.op2String = RmRegName;
            else // there was shift. include the shift string
                this.op2String = RmRegName + ", " + shiftName + " #" + shiftNum;

            return op2String;
        }

        internal uint getRmRegVal()
        {
            return RmRegVal;
        }

        internal uint getShiftNum()
        {
            return shiftNum;
        }
    }

    class RegShReg
    {
        private Memory memory;
        private Registers registers;
        private string shiftName;
        private string op2String;

        private uint instruction;
        private uint instructAddress;

        private uint shiftTypeUInt; // contains the value of a register shifted by an immediate
        private uint shiftedRegUInt;

        private uint Rm;        // Rm holds the register number
        private uint RmRegVal;  // RmRegVal holds register actual value
        private uint Rs;
        private uint RsRegVal; 
        

        public RegShReg(Memory _memory, Registers _registers, uint _currentInstruction, uint _currentInstAddress)
        {
            this.shiftName = "Not Known Yet";
            this.instruction = _currentInstruction;
            this.instructAddress = _currentInstAddress;

            // hook up registers and mememory object references
            this.registers = _registers;
            this.memory = _memory;
        }

        // HELPER FUCNTION: - to decode_Imm()
        //                  - decodes the shift name from the shift type
        // RETURNS:         -returns the shift name string
        //                  -returns "Invalid Shift Type" if it is an invalid type
        public string getShiftNameString(uint _shiftTypeUInt, uint _shiftNum)
        {
            // return string according to shift type bits
            //                          bits:  6  5
            switch (_shiftTypeUInt)
            {
                case 0:
                    return "lsl";           // 0  0
                case 1:
                    return "lsr";           // 0  1
                case 2:
                    return "asr";           // 1  0
                case 3:
                    if (_shiftNum == 0)
                        return "rrx";       // 1  1
                    else
                        return "ror";       // 1  1
                default:
                    Debug.WriteLine("ERROR in file 'Operand2' RegAndImmShReg.getShiftNameString(): type is out of range.");
                    return "Invalid Shift Type";
            }
        }

        // FUNCTION: 
        //      -Using instruction field update fields:  shiftNum, shiftTypeUInt,shiftName, Rm
        //      -decodes operand2 as Register with Immediate Shift
        public void decode_RegShReg()
        {

            // update field Rs
            Rs = (instruction >> 8) & 0x0000000f;

            // update field shiftTypeUInt
            shiftTypeUInt = (instruction >> 5) & 0x3;

            // update field shiftName
            uint bit11_7 = (instruction >> 7) & 0x0000001f;
            shiftName = getShiftNameString(shiftTypeUInt, bit11_7);

            // update field Rm
            Rm = instruction & 0x0000000f;

            // update field RnmRegVal
            // if Rm is PC, then RnRegVal should be currentAddress + 8 bytes. else it it's the content of register Rm
            if (Rm == 15)
                RmRegVal = instructAddress + 8;
            else
                RmRegVal = registers.getRegNValue(Rm);

            // update Rs if it is PC
            if (Rs == 15)
                RsRegVal = instructAddress + 8;
            else
                RsRegVal = registers.getRegNValue(Rs);

        }

        // FUNCTION:- execute RegAndImmShReg operand2
        //          - update field RmRegVal which contains the Rm register actual content value
        public void execute_RegShReg()
        {

            // shift XReg by shiftNum accoding to shift type bits.
            switch (shiftTypeUInt)                                // bits:  6  5
            {
                case 0:
                    shiftedRegUInt = BarrelShifter.lsl(RmRegVal, RsRegVal);            // 0  0 = LSL
                    break;
                case 1:
                    shiftedRegUInt = BarrelShifter.lsr(RmRegVal, RsRegVal);            // 0  1 = LSR
                    break;
                case 2:
                    shiftedRegUInt = BarrelShifter.asr(RmRegVal, RsRegVal);            // 1  0 = ASR
                    break;
                case 3:
                    /* if (_shiftNum == 0) TODO: this no supported for sim I
                         XReg = BarrelShifter.lsl(XReg, this.shiftNum);           // 1  1 = RRX
                     else*/
                    shiftedRegUInt = BarrelShifter.ror(RmRegVal, RsRegVal);        // 1  1 = ROR
                    break;
                default:
                    Debug.WriteLine("ERROR in file 'Operand2' RegAndImmShReg.execute_Imm(): type is out of range.");
                    break;
            }

            //this.shiftedRegUInt = XReg;
            // write back the copy into the register specified by Rm only if write back specified
            // this.registers.updateRegisterN(this.Rm, XReg);
        }

        internal uint getRmRegVal()
        {
            return RmRegVal;
        }

        internal uint getRsRegVal()
        {
            return RsRegVal;
        }

        internal uint getShiftedRegUInt()
        {
            return shiftedRegUInt;
        }

        internal string getOp2String()
        {
            string RmRegName = registers.getRegisterName(Rm);
            string RsRegName = registers.getRegisterName(Rs);

            this.op2String = RmRegName + ", " + shiftName + " " + RsRegName;

            return op2String;
        }
    }
}