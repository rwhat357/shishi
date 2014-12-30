using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace armsim
{
    // This class contains helper methods to decode and execute instructions.
    //
    public static class Extras
    {
        /* Check the instruction given and return whether it is a MUL instruction or not */
        public static bool isMulInstruction(uint inst)
        {
            uint bit27_to_bit21Val = (inst >> 21 ) & 0x7f; // get bits 27-21

            uint bit7_to_bit4Val = (inst >> 4) & 0xf; // get bits 7-4 

            // It's a MUL if bits 27-21 = 0 and bits 7-4 = 9
            return (bit27_to_bit21Val == 0 && bit7_to_bit4Val == 9)? true: false;
        }

        // Check the instruction given and return whether it is a SWI instruction or not
        public static bool isSWIInstruction(uint inst)
        {
            // check for SWI instruction. SWI instruction if bits 27--24 = 0xf
            return ((inst >> 24) & 0xf) == 0xf ? true : false;

        }

        // Check the instruction given and return whether it is a BX instruction or not
        public static bool isBX(uint inst)
        {
            uint bits_27_20 = (inst >> 20) & 0xff;
            uint bits_7_4 = (inst >> 4) & 0xf;

            if ((bits_27_20 == 18) && (bits_7_4 == 1)) // this situation: cond 0 0 0 1 0 0 1 0 SBO SBO SBO 0 0 0 1 Rm
                return true;

            return false;
        }

        // RECEIVES: an uint instruction
        // RETURSNS: the type of the instruction bits 27...25
        public static uint getNormalInstructionType(uint inst)
        {

            return (inst >> 25) & 0x7;
        }

        // RECEIVES: a uint 32 bit instruction, the NZCV flags
        // RETURNS:  true if instruction is a NOP, otherwise false. 
        public static bool isNOPInstruction(uint instruct, uint N, uint Z, uint C, uint V)
        {
            // Set up condition opcode with flags
            uint cond_opcode = (instruct >> 28) & 0xf;

            // Checks NZCF register flags to determine if it is a NOP instruction.
            // Check condition opcode to check which flags should be set.
            switch (cond_opcode)
            {
                // 0000 EQ Equal Z set
                // return false if the instruction shuld be executed. or is not a nop
                case 0:
                    if (Z == 1)
                        return false;
                    break;

                // 0001 NE Not equal Z clear
                case 1:
                    if (Z == 0)
                        return false;
                    break;

                //0010 CS/HS Carry set/unsigned higher or same C set
                case 2:
                    if (C == 1)
                        return false;
                    break;

                // 0011 CC/LO Carry clear/unsigned lower C clear
                case 3:
                    if (C == 0)
                        return false;
                    break;

                // 0100 MI Minus/negative N set
                case 4:
                    if (N == 1)
                        return false;
                    break;

                // 0101 PL Plus/positive or zero N clear
                case 5:
                    if (N == 0)
                        return false;
                    break;

                // 0110 VS Overflow V set
                case 6:
                    if (V == 1)
                        return false;
                    break;

                // 0111 VC No overflow V clear
                case 7:
                    if (V == 0)
                        return false;
                    break;

                // 1000 HI Unsigned higher C set and Z clear
                case 8:
                    if (Z == 0)
                        return false;
                    break;

                // 1001 LS Unsigned lower or same C clear or Z set
                case 9:
                    if (  (C == 0) || (Z == 1) )
                        return false;
                    break;

                // 1010 GE Signed greater than or equal N set and V set, or N clear and V clear (N == V)
                case 10:
                    if (N == V)
                        return false;
                    break;

                // 1011 LT Signed less than N set and V clear, or N clear and V set (N != V)
                case 11:
                    if (N != V)
                        return false;
                    break;

                // 1100 GT Signed greater than Z clear, and either N set and V set, or N clear and V clear (Z == 0,N == V)
                case 12:
                    if ((Z == 0) && (N == V))
                        return false;
                    break;

                // 1101 LE Signed less than or equal Z set, or N set and V clear, or N clear and V set (Z == 1 or N != V)
                case 13:
                    if ((Z == 1) || (N != V))
                        return false;
                    break;

                // 1110 AL Always (unconditional)
                case 14:
                    // always execute
                    return false;
                    

                // 1111 - See Condition code 0b1111
                case 15:
                    Console.WriteLine("CPU.isNOPInstruction(): special conditional opcode");
                    return false;
                    

            } // end switch

            // It's a nop instruction
            return true;
        }

        // RECEIVES: a uint 32 bit instruction and the NZCV flags
        // RETURNS:  a string that contains the conditional suffixes for an instruction (like eq, ne, etc)
        public static string getConditionalInstructionSuffix(uint instruct, uint N, uint Z, uint C, uint V)
        {
            // Set up condition opcode with flags
            uint cond_opcode = (instruct >> 28) & 0xf;

            // Checks NZCF register flags to determine if it is a NOP instruction.
            // Check condition opcode to check which flags should be set.
            switch (cond_opcode)
            {
                // 0000 EQ Equal Z set
                // return false if the instruction shuld be executed. or is not a nop
                case 0:
                    return "eq";

                // 0001 NE Not equal Z clear
                case 1:
                    return "ne";

                //0010 CS/HS Carry set/unsigned higher or same C set
                case 2:
                    return "cs";

                // 0011 CC/LO Carry clear/unsigned lower C clear
                case 3:
                    return "cc";

                // 0100 MI Minus/negative N set
                case 4:
                    return "mi";

                // 0101 PL Plus/positive or zero N clear
                case 5:
                    return "pl";

                // 0110 VS Overflow V set
                case 6:
                    return "vs";

                // 0111 VC No overflow V clear
                case 7:
                    return "vc";

                // 1000 HI Unsigned higher C set and Z clear
                case 8:
                    return "hi";

                // 1001 LS Unsigned lower or same C clear or Z set
                case 9:
                    return "ls";

                // 1010 GE Signed greater than or equal N set and V set, or N clear and V clear (N == V)
                case 10:
                    return "ge";

                // 1011 LT Signed less than N set and V clear, or N clear and V set (N != V)
                case 11:
                    return "lt";

                // 1100 GT Signed greater than Z clear, and either N set and V set, or N clear and V clear (Z == 0,N == V)
                case 12:
                    return "gt";

                // 1101 LE Signed less than or equal Z set, or N set and V clear, or N clear and V set (Z == 1 or N != V)
                case 13:
                    return "le";

                // 1110 AL Always (unconditional)
                case 14:
                    return ""; 


                // 1111 - See Condition code 0b1111
                case 15:
                    Console.WriteLine("CPU.isNOPInstruction(): special conditional opcode");
                    return "";

            } // end switch
            
            return "";
        }

    }// end of class
}