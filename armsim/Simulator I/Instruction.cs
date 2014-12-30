using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace armsim
{
    public abstract class  Instruction
    {
        // FUNCTION: Checks for special cases like MUL and then checks for instruction type
        //           from <instruction> and decodes it according to the instruction type.
        public abstract void decodeInstruction();

        public abstract void executeInstruction();
    }
}