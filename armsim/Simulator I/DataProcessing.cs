using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace armsim
{
    // decodes and executes data processing instrucitons
    class DataProcessing
    {
        private Memory memory;
        private Registers registers;

        private Immediate immediate;                  // operand2 type
        private RegAndImmShReg reg_and_imm_sh_reg;    // operand2 type
        private RegShReg reg_sh_reg;                  // operand2 type
        private string instructionString;
        private uint opcode;
        private uint S;

        private uint Rn;       // Rn and Rd hold the register numbers
        private uint Rd;
        private uint RnRegVal; // RnRegVal holds register actual value


        private uint instruction;
        private uint instructAddress;
        private bool bit25; //data processing addressing mode flag
        private bool bit4;  //data processing addressing mode flag

        public DataProcessing(Memory _memory, Registers _registers, uint _currentInstruction, uint _currentInstAddress)
        {
            this.instructionString = "Not Found Yet";
            this.instruction = _currentInstruction;
            this.instructAddress = _currentInstAddress;

            // initialize operand2 types to null
            this.immediate = null;
            this.reg_and_imm_sh_reg = null;
            this.reg_sh_reg = null;

            // hook up registers and mememory object references
            this.registers = _registers;
            this.memory = _memory;

            // setting up data processing addressing mode flags // -4 because pc gor incremented by 4 bytes when the instruction was fetched
            this.bit25 = memory.TestFlag(registers.getProgramCounter() - 4, 25);  //data processing addressing mode flag
            this.bit4 = memory.TestFlag(registers.getProgramCounter() -4 , 4);    //data processing addressing mode flag

        }

        /*********************************************************** Execute and Decode Methods *******************************************************/
        // FUNCTION: 
        //      -Using opcode field execute instruction using fields: Rn, Rd, operand2, 
        public void executeDP()
        {

            // update field instructionName by using field opcode
            switch (this.opcode)
            {
                case 0: // AND
                    this.AND();
                    break;
                case 1: // EOR
                    this.EOR();
                    break;
                case 2: // SUB
                    this.SUB();
                    break;
                case 3: // RSB
                    this.RSB();
                    break;
                case 4: // ADD
                    this.ADD();
                    break;
                /*
                case 5: // ADC
                    this.ADC();
                    break;
                case 6: // SBC
                    this.SBC();
                    break;
                case 7: // RSC
                    this.RSC();
                    break;
                case 8: // TST
                    this.TST();
                    break;
                case 9: // TEQ
                    this.TEQ();
                    break;
                */
                case 10: // CMP
                    this.CMP();
                    break;
                /*
                case 11: // CMN
                    this.CMN();
                    break;
                */
                case 12: // ORR
                    this.ORR();
                    break;
                case 13: // MOV
                    this.MOV();
                    break;
                case 14: // BIC
                    this.BIC();
                    break;
                case 15: // MVN
                    this.MVN();
                    break;

                default:
                    Console.WriteLine("ERROR: in updateInstructionName(): this is not a valid opcode for a data processing instruction.");
                    break;
            }
        }



        // FUNCTION: 
        //      -Using instruction field update fields: instructionString, opcode, Rn, Rd, operand2.
        public  void decodeDP()
        {

            // update field: instructionString, opcode
            opcode = (instruction >> 21) & 0xf;
            updateInstructionName();

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


            /*  Decode the operand2 of the intruction and update field operand2
             *  Steps to do this:
             *    1. check bits 25 & 4 for operand 2 types
             *    2. if 8 bit immediate
             *            initialize _8BitImmediate object and decode_Imm
             *       if register or immediate shifted register
             *            initialize  RegAndImmShReg object and decode_Imm
             *       if register shifted register
             *            initialize RegShReg object and decode_Imm
             */
            if (bit25 == true)                        // Immediate
            {
                immediate = new Immediate(memory, registers, instruction, instructAddress);
                immediate.decode_Imm();
            }


            if (bit25 == false && bit4 == false) // Register and Immediate Shifted Register
            {
                reg_and_imm_sh_reg = new RegAndImmShReg(memory, registers, instruction, instructAddress);
                reg_and_imm_sh_reg.decode_RegAndImmShReg();
            }

            // TODO: not implemented in sim I
            if (this.bit25 == false && this.bit4 == true)  // Register Shifted Register
            {
                this.reg_sh_reg = new RegShReg(this.memory, this.registers, this.instruction, this.instructAddress);
                this.reg_sh_reg.decode_RegShReg();
            }
             


        }

        // HELPER FUNCTION: to decodeLaS() 
        //                  -update field instructionName by using field opcode
        private void updateInstructionName()
        {

            // update field instructionName by using field opcode
            switch (this.opcode)
            {
                case 0:
                    this.instructionString = "and";
                    break;
                case 1:
                    this.instructionString = "eor";
                    break;
                case 2:
                    this.instructionString = "sub";
                    break;
                case 3:
                    this.instructionString = "rsb";
                    break;
                case 4:
                    this.instructionString = "add";
                    break;
                case 5:
                    this.instructionString = "adc";
                    break;
                case 6:
                    this.instructionString = "sbc";
                    break;
                case 7:
                    this.instructionString = "rsc";
                    break;
                case 8:
                    this.instructionString = "tst";
                    break;
                case 9:
                    this.instructionString = "teq";
                    break;
                case 10:
                    this.instructionString = "cmp";
                    break;
                case 11:
                    this.instructionString = "cmn";
                    break;
                case 12:
                    this.instructionString = "orr";
                    break;
                case 13:
                    this.instructionString = "mov";
                    break;
                case 14:
                    this.instructionString = "bic";
                    break;
                case 15:
                    this.instructionString = "mvn";
                    break;
                case 16:
                    this.instructionString = "mul";
                    break;

                default:
                    Console.WriteLine("ERROR: in updateInstructionName(): this is not a valid opcode for a data processing instruction.");
                    break;
            }
        }


        /*********************************************************** Data Processing Instructions *******************************************************/
        // BIC (Bit Clear) performs a bitwise AND of one value with the complement of a second value. The first value
        // comes from a register. The second value can be either an immediate value or a value from a register, and can
        // be shifted before the BIC operation.
        // BIC can optionally update the condition code flags, based on the result.
        // SYNTAX: BIC{<cond>}{S} <Rd>, <Rn>, <shifter_operand>
        // USES FIELDS: Rd, Rn, RnRegVal operand2
        private void BIC()
        {
            //Console.WriteLine("DataProcessing.BIC(): is a bic inst");
            // Immediate
            if (bit25 == true)
            {
                uint Rn_BIC_Op2 = RnRegVal & (~immediate.getRotatedImmediate());
                registers.updateRegisterN(Rd, Rn_BIC_Op2);
                instructionString += " " + registers.getRegisterName(Rd) + ", " + immediate.getImmString();
            }


            // Register and Immediate Shifted Register
            if (bit25 == false && bit4 == false)
            {
                // do shift on reg (told by Rm) by shiftNum
                reg_and_imm_sh_reg.execute_RegAndImmShReg();

                // do operation requested
                uint shiftedRegUInt = reg_and_imm_sh_reg.getRmRegVal();
                uint Rn_BIC_Op2 = RnRegVal & (~shiftedRegUInt);

                // move shifted register value into into a register
                registers.updateRegisterN(Rd, Rn_BIC_Op2);

                string op2String = reg_and_imm_sh_reg.getOp2String();
                instructionString += " " + registers.getRegisterName(Rd) + ", " + op2String;
            }


            // Register Shifted Register
            if (bit25 == false && bit4 == true)
            {
                //Console.WriteLine("DataProcesing.BIC(): inst. is a Register Shifted Register ");
                reg_sh_reg.execute_RegShReg();

                // move rototed reg val by reg
                uint shiftedRegUInt = reg_sh_reg.getShiftedRegUInt();
                uint Rn_BIC_Op2 = RnRegVal & (~shiftedRegUInt);
                registers.updateRegisterN(Rd, shiftedRegUInt);

                string op2String = reg_sh_reg.getOp2String();
                instructionString += " " + registers.getRegisterName(Rd) + ", " + op2String;

            }
        }
        
        
        
        /// FUNCTION: 
        ///       - MOV (Move) writes a value to the destination register. The value can be either an immediate value or a value
        ///         from a register, and can be shifted before the write.
        ///       - MOV can optionally update the condition code flags, based on the result.
        /// SYNTAX: MOV{<cond>}{S} <Rd>, <shifter_operand>
        /// USES FIELDS: Rd, operand2
        public void MOV()
        {
            // Immediate
            if (bit25 == true) 
            {
                registers.updateRegisterN(Rd, immediate.getRotatedImmediate());
                instructionString += " " + registers.getRegisterName(Rd) + ", " + immediate.getImmString();
            }


            // Register and Immediate Shifted Register
            if (bit25 == false && bit4 == false)
            { 
                // do shift on reg (told by Rm) by shiftNum
                reg_and_imm_sh_reg.execute_RegAndImmShReg();

                // do operation requested
                uint shiftedRegUInt = reg_and_imm_sh_reg.getRmRegVal();
                

                // move shifted register value into into a register
                registers.updateRegisterN(Rd, shiftedRegUInt);

                string op2String = reg_and_imm_sh_reg.getOp2String();
                instructionString += " " + registers.getRegisterName(Rd) + ", " + op2String;
            }


            // Register Shifted Register
            if (bit25 == false && bit4 == true)
            {
                //Console.WriteLine("DataProcesing.MOV(): inst. is a Register Shifted Register ");
                reg_sh_reg.execute_RegShReg();

                // move rototed reg val by reg
                uint shiftedRegUInt = reg_sh_reg.getShiftedRegUInt();
                registers.updateRegisterN(Rd, shiftedRegUInt);
               
                string op2String = reg_sh_reg.getOp2String();
                instructionString += " " + registers.getRegisterName(Rd) + ", " + op2String;

            }
             
        }


        /// FUNCTION: 
        ///       - MVN (Move Not) generates the logical ones complement of a value. The value can be either an immediate
        ///         value or a value from a register, and can be shifted before the MVN operation.
        ///       - MVN can optionally update the condition code flags, based on the result.
        /// SYNTAX: MVN{<cond>}{S} <Rd>, <shifter_operand>
        /// USES FIELDS: Rd, operand2
        public void MVN()
        {
            // Immediate
            if (bit25 == true)
            {
                uint notted_RotatedImmediate = ~( immediate.getRotatedImmediate() );
                registers.updateRegisterN(Rd, notted_RotatedImmediate);
                instructionString += " " + registers.getRegisterName(Rd) + ", " + immediate.getImmString();
            }


            // Register and Immediate Shifted Register
            if (bit25 == false && bit4 == false)
            { 

                // do shift on reg (told by Rm) by shiftNum
                reg_and_imm_sh_reg.execute_RegAndImmShReg();

                // do operation requested
                uint nottedShiftedRegUInt = ~( reg_and_imm_sh_reg.getRmRegVal() );
                string op2String = reg_and_imm_sh_reg.getOp2String();

                // move notted shifted register value into into a register
                registers.updateRegisterN(Rd, nottedShiftedRegUInt);

                instructionString += " " + registers.getRegisterName(Rd) + ", " + op2String;
            }


            // Register Shifted Register
            /* TODO: not implemented for simI
            if (bit25 == false && bit4 == true)  
                registers.updateRegisterN(Rd, immediate.getRotatedImmediate());
             */
        }

        /// FUNCTION: 
        ///       - ADD adds two values. The first value comes from a register. The second value can be either an immediate
        ///         value or a value from a register, and can be shifted before the addition.
        ///       - ADD can optionally update the condition code flags, based on the result.
        /// SYNTAX: ADD{<cond>}{S} <Rd>, <Rn>, <shifter_operand>
        /// USES FIELDS: Rd, operand2
        public void ADD()
        {
            // Immediate
            if (bit25 == true)
            {
                uint Rn_plus_imm = RnRegVal + immediate.getRotatedImmediate();
                registers.updateRegisterN(Rd, Rn_plus_imm);
                instructionString += " " + registers.getRegisterName(Rd) + ", " + registers.getRegisterName(Rn)  + ", " + immediate.getImmString();
            }


            // Register and Immediate Shifted Register
            if (bit25 == false && bit4 == false)
            { 
                // do shift on reg (told by Rm) by shiftNum
                reg_and_imm_sh_reg.execute_RegAndImmShReg();

                // do operation requested
                uint Rn_plus_shiftedRegUInt = RnRegVal + reg_and_imm_sh_reg.getRmRegVal();

                // move added value into into a register
                registers.updateRegisterN(Rd, Rn_plus_shiftedRegUInt);

                // update the instruction string
                string op2String = reg_and_imm_sh_reg.getOp2String();
                instructionString += " " + registers.getRegisterName(Rd) + ", " + registers.getRegisterName(Rn) + ", " + op2String;
            }


            // Register Shifted Register
            /* TODO: not implemented for simI
            if (bit25 == false && bit4 == true)  
                registers.updateRegisterN(Rd, immediate.getRotatedImmediate());
             */
        }

        /// FUNCTION: 
        ///       - SUB (Subtract) subtracts one value from a second value.
        ///         The second value comes from a register. The first value can be either an immediate value or a value from a
        ///         register, and can be shifted before the subtraction.
        ///       - SUB can optionally update the condition code flags, based on the result.
        /// SYNTAX: SUB{<cond>}{S} <Rd>, <Rn>, <shifter_operand>
        /// USES FIELDS: Rd, operand2
        public void SUB()
        {
            // Immediate
            if (bit25 == true)
            {
                uint Rn_minus_imm = RnRegVal - immediate.getRotatedImmediate();
                registers.updateRegisterN(Rd, Rn_minus_imm);
                instructionString += " " + registers.getRegisterName(Rd) + ", " + registers.getRegisterName(Rn) + ", " + immediate.getImmString();
            }


            // Register and Immediate Shifted Register
            if (bit25 == false && bit4 == false)
            {
                // do shift on reg (told by Rm) by shiftNum
                reg_and_imm_sh_reg.execute_RegAndImmShReg();

                // do operation requested
                uint Rn_minus_RmRegVal = RnRegVal - reg_and_imm_sh_reg.getRmRegVal();

                // move new value into into a register
                registers.updateRegisterN(Rd, Rn_minus_RmRegVal);

                // update the instruction string
                string op2String = reg_and_imm_sh_reg.getOp2String();
                instructionString += " " + registers.getRegisterName(Rd) + ", " + registers.getRegisterName(Rn) + ", " + op2String;
            }


            // Register Shifted Register
            /* TODO: not implemented for simI
            if (bit25 == false && bit4 == true)  
                registers.updateRegisterN(Rd, immediate.getRotatedImmediate());
             */
        }

        /// FUNCTION: 
        ///       - RSB (Reverse Subtract) subtracts a value from a second value.
        ///       - The first value comes from a register. The second value can be either an immediate value or a value from a
        ///         register, and can be shifted before the subtraction. This is the reverse of the normal order of operands in
        ///         ARM assembler language.
        ///       - RSB can optionally update the condition code flags, based on the result.
        /// SYNTAX: SUB{<cond>}{S} <Rd>, <Rn>, <shifter_operand>
        /// USES FIELDS: Rd, operand2
        public void RSB()
        {
            // Immediate
            if (bit25 == true)
            {
                uint Rn_minus_imm = immediate.getRotatedImmediate() - RnRegVal;
                registers.updateRegisterN(Rd, Rn_minus_imm);
                instructionString += " " + registers.getRegisterName(Rd) + ", " + registers.getRegisterName(Rn) + ", " + immediate.getImmString();
            }


            // Register and Immediate Shifted Register
            if (bit25 == false && bit4 == false)
            {
                // do shift on reg (told by Rm) by shiftNum
                reg_and_imm_sh_reg.execute_RegAndImmShReg();

                // do operation requested
                uint Rn_minus_RmRegVal = reg_and_imm_sh_reg.getRmRegVal() - RnRegVal;

                // move new value into into a register
                registers.updateRegisterN(Rd, Rn_minus_RmRegVal);

                // update the instruction string
                string op2String = reg_and_imm_sh_reg.getOp2String();
                instructionString += " " + registers.getRegisterName(Rd) + ", " + registers.getRegisterName(Rn) + ", " + op2String;
            }


            // Register Shifted Register
            /* TODO: not implemented for simI
            if (bit25 == false && bit4 == true)  
                registers.updateRegisterN(Rd, immediate.getRotatedImmediate());
             */
        }

        /// FUNCTION: 
        ///       - AND performs a bitwise AND of two values. The first value comes from a register. The second value can be
        ///         either an immediate value or a value from a register, and can be shifted before the AND operation.
        ///       - AND can optionally update the condition code flags, based on the result.
        /// SYNTAX: AND{<cond>}{S} <Rd>, <Rn>, <shifter_operand>
        /// USES FIELDS: Rd, operand2
        public void AND()
        {
            // Immediate
            if (bit25 == true)
            {
                uint Rn_AND_imm =  RnRegVal & immediate.getRotatedImmediate();
                registers.updateRegisterN(Rd, Rn_AND_imm);
                instructionString += " " + registers.getRegisterName(Rd) + ", " + registers.getRegisterName(Rn) + ", " + immediate.getImmString();
            }


            // Register and Immediate Shifted Register
            if (bit25 == false && bit4 == false)
            {
                // do shift on reg (told by Rm) by shiftNum
                reg_and_imm_sh_reg.execute_RegAndImmShReg();

                // do operation requested
                uint Rn_AND_RmRegVal = RnRegVal & reg_and_imm_sh_reg.getRmRegVal();

                // move new value into into a register
                registers.updateRegisterN(Rd, Rn_AND_RmRegVal);

                // update the instruction string
                string op2String = reg_and_imm_sh_reg.getOp2String();
                instructionString += " " + registers.getRegisterName(Rd) + ", " + registers.getRegisterName(Rn) + ", " + op2String;
            }


            // Register Shifted Register
            /* TODO: not implemented for simI
            if (bit25 == false && bit4 == true)  
                registers.updateRegisterN(Rd, immediate.getRotatedImmediate());
             */
        }

        /// FUNCTION: 
        ///       - ORR (Logical OR) performs a bitwise (inclusive) OR of two values. The first value comes from a register.
        ///         The second value can be either an immediate value or a value from a register, and can be shifted before the
        ///         OR operation.
        ///       - ORR can optionally update the condition code flags, based on the result.
        /// SYNTAX: ORR{<cond>}{S} <Rd>, <Rn>, <shifter_operand>
        /// USES FIELDS: Rd, operand2
        public void ORR()
        {
            // Immediate
            if (bit25 == true)
            {
                uint Rn_ORR_imm = RnRegVal | immediate.getRotatedImmediate();
                registers.updateRegisterN(Rd, Rn_ORR_imm);
                instructionString += " " + registers.getRegisterName(Rd) + ", " + registers.getRegisterName(Rn) + ", " + immediate.getImmString();
            }


            // Register and Immediate Shifted Register
            if (bit25 == false && bit4 == false)
            {
                // do shift on reg (told by Rm) by shiftNum
                reg_and_imm_sh_reg.execute_RegAndImmShReg();

                // do operation requested
                uint Rn_OR_RmRegVal = RnRegVal | reg_and_imm_sh_reg.getRmRegVal();

                // move new value into into a register
                registers.updateRegisterN(Rd, Rn_OR_RmRegVal);

                // update the instruction string
                string op2String = reg_and_imm_sh_reg.getOp2String();
                instructionString += " " + registers.getRegisterName(Rd) + ", " + registers.getRegisterName(Rn) + ", " + op2String;
            }


            // Register Shifted Register
            /* TODO: not implemented for simI
            if (bit25 == false && bit4 == true)  
                registers.updateRegisterN(Rd, immediate.getRotatedImmediate());
             */
        }

        /// FUNCTION: 
        ///       - EOR (Exclusive OR = XOR) performs a bitwise Exclusive-OR of two values. The first value comes from a register.
        ///         The second value can be either an immediate value or a value from a register, and can be shifted before the
        ///         exclusive OR operation.
        ///       - EOR can optionally update the condition code flags, based on the result.
        /// SYNTAX: EOR{<cond>}{S} <Rd>, <Rn>, <shifter_operand>
        /// USES FIELDS: Rd, operand2
        public void EOR()
        {
            // Immediate
            if (bit25 == true)
            {
                uint Rn_EOR_imm = RnRegVal ^ immediate.getRotatedImmediate();
                registers.updateRegisterN(Rd, Rn_EOR_imm);
                instructionString += " " + registers.getRegisterName(Rd) + ", " + registers.getRegisterName(Rn) + ", " + immediate.getImmString();
            }


            // Register and Immediate Shifted Register
            if (bit25 == false && bit4 == false)
            {
                // do shift on reg (told by Rm) by shiftNum
                reg_and_imm_sh_reg.execute_RegAndImmShReg();

                // do operation requested
                uint Rn_EOR_RmRegVal = RnRegVal ^ reg_and_imm_sh_reg.getRmRegVal();

                // move new value into into a register
                registers.updateRegisterN(Rd, Rn_EOR_RmRegVal);

                // update the instruction string
                string op2String = reg_and_imm_sh_reg.getOp2String();
                instructionString += " " + registers.getRegisterName(Rd) + ", " + registers.getRegisterName(Rn) + ", " + op2String;
            }


            // Register Shifted Register
            /* TODO: not implemented for simI
            if (bit25 == false && bit4 == true)  
                registers.updateRegisterN(Rd, immediate.getRotatedImmediate());
             */
        }

        /// FUNCTION: 
        ///       - CMP (Compare) compares two values. The first value comes from a register. The second value can be either
        ///         an immediate value or a value from a register, and can be shifted before the comparison.
        ///       - CMP updates the condition flags, based on the result of subtracting the second value from the first.
        /// SYNTAX: CMP{<cond>} <Rn>, <shifter_operand>
        /// USES FIELDS: Rn, operand2
        private void CMP()
        {
            ///  Four condition flags:
            ///  N = bit 31 of the result of the arithmetic operation (sign bit)
            ///  	 1 if result is negative
            ///  	 0 if result is positive
            ///  Z = 1 if the result is 0, 0 if the result is non zero
            ///  C = Carry flag
            ///  V = Overflow flag

            // Rn and shifter operands to check CMP
            uint cmpVal_uint = 0; // flags are set accoding to this result
            int  cmpVal_int = 0;

            uint RnVal_uint = 0;
            uint shifter_operand_uint = 0;

            int  RnVal_int = 0;
            int  shifter_operand_int = 0;

            //========================= ===Set locals to later set flags ==================================//
            // Immediate
            if (bit25 == true)
            {
                // set all locals to check flags
                RnVal_uint = RnRegVal;
                shifter_operand_uint = immediate.getRotatedImmediate();

                RnVal_int = (int)RnRegVal;
                shifter_operand_int = (int)immediate.getRotatedImmediate();

                cmpVal_uint = RnVal_uint - shifter_operand_uint;
                cmpVal_int = RnVal_int - shifter_operand_int;

                // update disassembly string
                instructionString += " " + registers.getRegisterName(Rn) + ", " + immediate.getImmString();
            }


            // Register and Immediate Shifted Register
            if (bit25 == false && bit4 == false)
            {
                // do shift on reg (told by Rm) by shiftNum
                reg_and_imm_sh_reg.execute_RegAndImmShReg();

                // set all locals to check flags
                RnVal_uint = RnRegVal;
                shifter_operand_uint = reg_and_imm_sh_reg.getRmRegVal();

                RnVal_int = (int)RnRegVal;
                shifter_operand_int = (int)reg_and_imm_sh_reg.getRmRegVal();

                cmpVal_uint = RnVal_uint - shifter_operand_uint;
                cmpVal_int = RnVal_int - shifter_operand_int;

                // update disassembly string
                string op2String = reg_and_imm_sh_reg.getOp2String();
                instructionString += " " + registers.getRegisterName(Rn) + ", " + op2String;
            }


            // Register Shifted Register
            /* TODO: not implemented for simI
            if (bit25 == false && bit4 == true)  
                registers.updateRegisterN(Rd, immediate.getRotatedImmediate());
             */

            //========================= ======  set flags ==================================//
            // set N flag
            uint bit_31 = (cmpVal_uint >> 31) & 0xffffffff;
            registers.setNFlag(bit_31);

            // set Z flag
            if (cmpVal_uint == 0)
                registers.setZFlag(1);
            else
                registers.setZFlag(0);

            ///  Computing C
            ///  Interpret <Rn> and <shifter_operand> value as unsigned quantities
            ///  In an unsigned subtraction, if <shifter_operand> is greater than <Rn>, the result would
            ///  underflow and become negative, requiring a “borrow.” C = 0 in this case.
            ///  If <shifter_operand> is less than or equal to <Rn>, the result would be positive or 0. C =
            ///  1 in this case.
 
            //  set C flag
            if (shifter_operand_uint > RnVal_uint)
                registers.setCFlag(0);
            else
                registers.setCFlag(1);

            ///  Computing V
            ///  Interpret <Rn> and <shifter_operand> value as signed quantities
            ///  Overflow occurs in the following two cases:
            ///  Case #1: <Rn> is positive, <shifter_operand> is negative, and the result is
            ///           negative
            ///  Case #2: <Rn> is negative, <shifter_operand> is positive, and the result is
            ///           positive

            // set V flag
            if ( (RnVal_int >= 0) &&            
                    (shifter_operand_int < 0) && 
                        (cmpVal_int < 0) )    // Case #1
                registers.setVFlag(1);

            else if ((RnVal_int < 0) &&
                      (shifter_operand_int >= 0) && 
                        (cmpVal_int >=0 ) )   // case #2
                registers.setVFlag(1);

            else // no overflow
                registers.setVFlag(0);

            return;
        }

        /***********************************************************End of Data Processing Instructions ****************************************************/

        public string getInstructionString() { return instructionString; }
    }
}
