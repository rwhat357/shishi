using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace armsim
{
    // Holds methods LSL, Logical Shift Left
    //               LSR, Logical Shift Right
    //               ASR, Arithmetic Shift Right
    //               ASL, Arithmetic Shift Left
    //               ROR, Rotate Right
    // All methods take parameters <instruct> and <num>
    //       <num> is always expected to be: 31 >= num >= 0, else the method. c
    //       being called returns a zero. <num> = number of bits
    public static class BarrelShifter
    {
        
        // FUNCTION: Logical Shift Left: shift left <instruct> by <num> bits. Fill lower bits with 0's
        // RETURN:   return shifted <instruct>
        public static uint lsl(uint instruct, uint num)
        {
            int IntNum = (int)num;

            // Fill wiht 1's or 0's if num > 31 since we can't fill everything with one shift
            // becasue << takes 31 as its maximun operand before slicing the operant to the lower 5 bits.
            if (num > 31)
            {
                instruct = instruct << 31; // shift left all bits, we have left 1 bit filled
                instruct = instruct << 1;  // get rid of the 1 bit 
            }

            instruct = instruct << IntNum;
            
            return (instruct);
        }

        // FUNCTION: Logical Shift Right: shift right <instruct> by <num> bits. Fill left bits with zeros.
        // RETURN:   return shifted <instruct>
        public static uint lsr(uint instruct, uint num)
        {
            // Steps to do lsr
            // - cast <instruct> to uint
            // - lsr inst by num bits
            // - cast <instrct> to int
            // - return int instr


            // Fill left bits with 0's if num > 31 since we can't fill everything with one shift
            // becasue >> takes 31 as its maximun operand before slicing the operant to the lower 5 bits.
            if (num > 31) // shift more than 31 bits
            {
                instruct = instruct >> 31; // shift right all bits, we have left 1 bit filled
                instruct = instruct >> 1;  // get rid of the 1 bit 

                return instruct;
            }

            int IntNum = (int)num;
            instruct = instruct >> IntNum;
            
            return (instruct);
        }

        // FUNCTION: Arithmetic Shift Right: shift right <instruct> by <num> bits and sign extend on the left bits
        // RETURN: shifted <instruct>
        public static uint asr(uint instruct, uint num)
        {
            int IntInstruct = (int)instruct;
            int IntNum = (int)num;

            // Fill wiht 1's or 0's if num > 31 since we can't fill everything with one shift
            // becasue >> takes 31 as its maximun operand before slicing the operant to the lower 5 bits.
            if (IntNum > 31)
            {
                instruct = instruct >> 31; // shift right all bits, we have left 1 bit filled
                instruct = instruct >> 1;  // get rid of the 1 bit 
            }

            IntInstruct = IntInstruct >> IntNum;
            instruct = (uint)IntInstruct;

            return instruct;
        }

        // FUNCTION: Arithmetic Shift Left: shift left <instruct> by <num> bits
        // RETURN:   return shifted <instruct>
        public static uint asl(uint instruct, uint num)
        {
            int IntNum = (int)num;

            // Fill wiht 1's or 0's if num > 31 since we can't fill everything with one shift
            // becasue << takes 31 as its maximun operand before slicing the operant to the lower 5 bits.
            if (IntNum > 31)
            {
                instruct = instruct << 31; // shift left all bits, we have left 1 bit filled
                instruct = instruct << 1;  // get rid of the 1 bit 
            }
            //Console.WriteLine("Hex: {0:X}", instruct);
            instruct = instruct << IntNum;
            //Console.WriteLine("Hex: {0:X}", temp);

            return instruct;
        }

        // FUNCTION:Rotate Right: rotate right <instruct> by <num> bits
        // RETURN: rotated <instruct>
        public static uint ror(uint instruct, uint num)
        {
            // convert instruction to uint perform ROR and cast it back to int to return it.

            int IntNum = (int)num;

            //Console.WriteLine("Hex: {0:X}", uInstruct);
            instruct = (instruct >> IntNum) | (instruct << (32 - IntNum)); // only works for uint
            //Console.WriteLine("Hex: {0:X}", uInstruct);

            return (instruct);
        }
    }
}
