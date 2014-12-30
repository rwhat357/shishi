using System;
using System.Diagnostics; // Trace() and Debug()
using armsim; // my custom namespace

public class Registers : Memory, Observer
{

    Subject subject;
    uint r0;
    uint r1;
    uint r2;
    uint r3;
    uint r4;
    uint r5;
    uint r6;
    uint r7;
    uint r8;
    uint r9;
    uint r10sl;
    uint r11fp;
    uint r12ip; // frame pointer
    uint r13sp; // stack poointer
    uint r14lr; // link register, holds the caller's return address
    uint r15pc; // program counter

    uint cpsr; // status register: CPSR Register (Current Program Status Register), holds control flags

    // control flags
    uint N, Z, C, V;

    public Registers()
    {   
        // initialize registers
         r0 = 0;
         r1 = 0;
         r2 = 0;
         r3 = 0;
         r4 = 0;
         r5 = 0;
         r6 = 0;
         r7 = 0;
         r8 = 0;
         r9 = 0;
         r10sl = 0;
         r11fp = 0;
         r12ip = 0;
         r13sp = 0;
         r14lr = 0;
         r15pc = 0;

        // initalize control flags
        N = 0; Z = 0; C = 0; V = 0;
        cpsr = 0;

    }

    public Registers(Subject _subject)
    {
        // initialize registers
        r0 = 0;
        r1 = 0;
        r2 = 0;
        r3 = 0;
        r4 = 0;
        r5 = 0;
        r6 = 0;
        r7 = 0;
        r8 = 0;
        r9 = 0;
        r10sl = 0;
        r11fp = 0;
        r12ip = 0;
        r13sp = 0;
        r14lr = 0;
        r15pc = 0;

        // initalize control flags
        N = 0; Z = 0; C = 0; V = 0;
        cpsr = 0;

    }
    // FUNCTION OVERRIDE IN OBSERVER: Empty method. This method is needed for observer pattern
    public void Notify_StopExecution()
    {

    }

    //FUNCTION OVERRIDE IN OBSERVER:: Empty method. This method is needed for observer pattern
    public void Notify_OneCycle()
    {

    }

    //FUNCTION OVERRIDE IN OBSERVER:: Empty method. This method is needed for observer pattern
    public void Notify_WriteCharToTerminal()
    {

    }



    public void updateProgramCounter(uint programCounter)
    {
        r15pc = programCounter;
    }

    public void incrementProgramCounterInAWordSize()
    {
        r15pc = r15pc + 4;
    }

    public void clearRegisters()
    {

        r0 = 0;
        r1 = 0;
        r2 = 0;
        r3 = 0;
        r4 = 0;
        r5 = 0;
        r6 = 0;
        r7 = 0;
        r8 = 0;
        r9 = 0;
        r10sl = 0;
        r11fp = 0;
        r12ip= 0;
        r13sp= 0;
        r14lr= 0;
        r15pc = 0;

    }


    internal uint getProgramCounter()
    {
        return  r15pc;
    }


    internal uint[] getRegistersIntArray()
    {
        uint[] registersArray = {   r0  , 
                                    r1  ,
                                    r2  ,
                                    r3  ,
                                    r4  ,
                                    r5  ,
                                    r6  ,
                                    r7  ,
                                    r8  ,
                                    r9  ,
                                    r10sl ,
                                    r11fp ,
                                    r12ip,
                                    r13sp,
                                    r14lr,
                                    r15pc  };



        return registersArray;
    }

    // return control flags in this order: N, Z, C, V 
    internal uint[] getControlFlagsIntArray()
    {
        uint[] controlFlagsIntArray = { N, Z, C, V };
        return controlFlagsIntArray;
    }


    internal string[] getRegisterNames()
    {
        string[] registerNames = {      "R0"   , 
                                        "R1"   ,                               
                                        "R2"   ,                               
                                        "R3"   ,                               
                                        "R4"   ,                               
                                        "R5"   ,                               
                                        "R6"   ,                               
                                        "R7"   ,                               
                                        "R8"   ,                               
                                        "R9"   ,                               
                                        "SL"  ,                               
                                        "FP"  ,                                
                                        "IP"   ,                               
                                        "SP"   ,                               
                                        "LR"   ,                               
                                        "PC"   };                              

        return registerNames;

    }

    // return control flags in this order: N, Z, C, V 
    internal string[] getControlFlagNames()
    {
        string[] controlFlagNamesArray = { "N", "Z", "C", "V" };
        return controlFlagNamesArray;
    }

    // FUNCTION: update or write value to register number.
    internal void updateRegisterN(uint regNum, uint value)
    {
        // update <register with value
        switch (regNum)
        {
            case 0:
                this.r0 = value;
                break;
            case 1:
                this.r1 = value;
                break;
            case 2:
                this.r2 = value;
                break;
            case 3:
                this.r3 = value;
                break;
            case 4:
                this.r4 = value;
                break;
            case 5:
                this.r5 = value;
                break;
            case 6:
                this.r6 = value;
                break;
            case 7:
                this.r7 = value;
                break;
            case 8:
                this.r8 = value;
                break;
            case 9:
                this.r9 = value;
                break;
            case 10:
                this.r10sl = value;
                break;
            case 11:
                this.r11fp = value;
                break;
            case 12:
                this.r12ip = value;
                break;
            case 13:
                this.r13sp = value;
                break;
            case 14:
                this.r14lr = value;
                break;
            case 15:
                this.r15pc = value;
                break;

            default:
                Debug.WriteLine("ERROR: in Registers.updateRegisterN(): this is not a valid register value (not in range 0-15).");
                break;
        } // end switch
    }

    // FUNCTION: receives a register number
    // RETURNS: - "ERROR" string if registerNum is not in ranget 0-15
    //          - returns the register name that corresponds to that number.
    internal string getRegisterName(uint registerNum)
    {
        // update <register with value
        switch (registerNum)
        {
            case 0:
                return "r0";
            case 1:
                return "r1";
            case 2:
                return "r2";
            case 3:     
                return "r3";
            case 4:     
                return "r4";
            case 5:
                return "r5";
            case 6:
                return "r6";
            case 7:     
                return "r7";
            case 8:     
                return "r8";
            case 9:     
                return "r9";
            case 10:    
                return "sl";
            case 11:    
                return "fp";
            case 12:    
                return "ip";  // FP
            case 13:    
                return "sp";  // SP
            case 14:    
                return "lr";  // LR
            case 15:    
                return "pc";  // PC

            default:
                Debug.WriteLine("ERROR: in Registers.getRegisterName(): this is not a valid register value (not in range 0-15).");
                return "ERROR";
        } // end switch
    

    }


    // FUNCTION: receives a register number
    // RETURNS: - returns the register content that correcponds to that number.
    //          - returns 0 if register number is not in range 0-15.
    internal uint getRegNValue(uint registerNum)
    {
        // update <register with value
        switch (registerNum)
        {
            case 0:
                return r0;
            case 1:
                return r1;
            case 2:
                return r2;
            case 3:
                return r3;
            case 4:
                return r4;
            case 5:
                return r5;
            case 6:
                return r6;
            case 7:
                return r7;
            case 8:
                return r8;
            case 9:
                return r9;
            case 10:
                return r10sl;
            case 11:
                return r11fp;
            case 12:
                return r12ip;
            case 13:
                return r13sp;
            case 14:
                return r14lr;
            case 15:
                return r15pc;

            default:
                Debug.WriteLine("ERROR: in Registers.getRegNValue(): this is not a valid register value (not in range 0-15).");
                return 0;
        } // end switch
    }

    internal void updateStackPointer(uint newStackPointer)
    {
        r13sp = newStackPointer;
    }

    internal uint getStackPointer()
    {
        return r13sp;
    }

    //******************************************************* NZCV flag getters and setters *****************************************//
    // Sets N flag (Negative flag) to <flag>
    internal void setNFlag(uint flag)
    {
        N = flag;
    }

    internal void setZFlag(uint flag)
    {
        Z = flag;
    }

    internal void setCFlag(uint flag)
    {
        C = flag;
    }

    internal void setVFlag(uint flag)
    {
        V = flag;
    }

    internal uint getNFlag()
    {
        return N;
    }

    internal uint getZFlag()
    {
        return Z;
    }

    internal uint getCFlag()
    {
        return C;
    }

    internal uint getVFlag()
    {
        return V;
    }

    internal void clearNZCVFlags()
    {
        N = 0;
        Z = 0;
        C = 0;
        V = 0;
    }
}