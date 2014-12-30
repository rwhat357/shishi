using System;
using System.Security.Cryptography;
using System.Diagnostics; // Trace() and Debug()
using armsim; // my custom namespace

public class Memory: Observer
{
    private Subject subject;
    uint RAMSize;
    private byte[] RAM;

    // default constructor
    public Memory()
    {
        uint RAMDefaultSize = 32768; // default 32KB or 32768 bytes
        RAM = new byte[RAMDefaultSize];
        RAMSize = RAMDefaultSize; 
    }

    // Constructor: creates RAM with specific size
    public Memory(uint _RAMsize)
    {
        RAM = new byte[_RAMsize];
        RAMSize = _RAMsize;
    }

    public Memory(Subject _subject, uint _RAMsize)
    {
        // tie reference to subject and add observer to observers list
        this.subject = _subject;
        this.subject.registerObserver(this);

        // initialize fields
        RAM = new byte[_RAMsize];
        this.subject = _subject;
        this.RAMSize = _RAMsize;
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



    // TODO: for now return int.MinValue
    // returns integer from RAM if <address> is divisible by 4
    // returns int.MaxValue if <address> is not divisible by 4
    // returns int.MaxValue if it was not able to read instruction
    public uint ReadWord(uint address)
    {
        if (!(address % 4 == 0)) // address not divisible by 4
            //return uint.MaxValue;
            return uint.MinValue;

        if (RAMSize <= address)  // address is greater than the RAM size
            //return uint.MaxValue;
            return uint.MinValue;

        byte[] bytes = { RAM[address], RAM[address + 1], RAM[address + 2], RAM[address + 3] }; // collect bytes from RAM into buffer

        uint retNum = BitConverter.ToUInt32(bytes, 0); // convert 4 bytes into uint
        return retNum;
    }

    // returns 0 if it was able to write <dataValue> into RAM
    // returns -1 if <address> is not divisible by 4
    // returns -1 if not able to write
    public int WriteWord(uint address, uint dataValue)
    {
        int intSizeInBytes = 4;

        if (!(address % 4 == 0)) // address not divisible by 4
            return -1;

        if ((RAM.Length - address) < intSizeInBytes) // RAM to small to fit the integer
            return -1;

        if (RAMSize < address)  // address is greater than the RAM size
            return -1;

        // Write integer to RAM
        byte[] bytes = BitConverter.GetBytes(dataValue); // retuns four bytes from int
        RAM[address + 0] = bytes[0];
        RAM[address + 1] = bytes[1];
        RAM[address + 2] = bytes[2];
        RAM[address + 3] = bytes[3];

        return 0;
    }

    // returns short from RAM if <address> is divisible by 2
    // returns short.MaxValue if <address> is not divisible by 2
    // returns short.MaxValue if it was not able to read half instruction.
    public ushort ReadHalfWord(uint address)
    {
        if (!(address % 2 == 0)) // address not divisible by 2
            return ushort.MaxValue;

        if (RAMSize <= address)  // address is greater or equal than the RAM size
            return ushort.MaxValue;

        byte[] bytes = { RAM[address], RAM[address + 1] }; // collect bytes from RAM into buffer

        ushort retNum = BitConverter.ToUInt16(bytes, 0); // convert 2 bytes into short
        return retNum;
    }

    // returns 0 if it was able to write <dataValue> into RAM
    // returns -1 if <address> is not divisible by 2
    // returns -1 if not able to write
    public int WriteHalfWord(uint address, ushort dataValue)
    {

        int shortSizeInBytes = 2; // HalfWord size

        if (!(address % 2 == 0)) // address not divisible by 2
            return -1;

        if ((RAM.Length - address) < shortSizeInBytes) // RAM to small to fit the short
            return -1;

        if (RAMSize < address)  // address is greater than the RAM size
            return -1;

        // Write short to RAM
        byte[] bytes = BitConverter.GetBytes(dataValue); // retuns 2 bytes from short
        RAM[address + 0] = bytes[0];
        RAM[address + 1] = bytes[1];

        return 0;
    }

    // returns byte from RAM if <address> is in the RAM range
    // returns byte.MaxValue if it was not able to read byte
    public byte ReadByte(uint address)
    {
        if (address == 0x00100001)
        {
            ArmSimFormRef.WriteCharToTerminal("Hello World!");
            return 0;
        }
            

        //if (RAMSize <= address)  // address is greater or equal to the RAM size
        //    return byte.MaxValue;

        return RAM[address];
    }

    // returns 0 if it was able to write <dataValue> into RAM
    // returns -1 if not able to write
    public int WriteByte(uint address, byte dataValue)
    {
        if (RAMSize <= address)  // address is greater or equal to the RAM size
            return -1;

        RAM[address] = dataValue;
        return 0;
    }

    // returns the RAM's hex digest in a string
    public string ComputeCurrentMD5RAM()
    {
        MD5 md5Hash = MD5.Create();
        byte[] hash = md5Hash.ComputeHash(RAM);

        string hexString = BitConverter.ToString(hash); // in format 00-00-00-00

        hexString = hexString.Replace("-", ""); // remove dashes (-)

        return (hexString);
    }

    // print binary representaion of number <n>
    // helper test function for SetFlag()
    public string GetIntBinaryString(int n)
    {
        char[] b = new char[32];
        int pos = 31;
        int i = 0;

        while (i < 32)
        {
            if ((n & (1 << i)) != 0)
            {
                b[pos] = '1';
            }
            else
            {
                b[pos] = '0';
            }
            pos--;
            i++;
        }
        return new string(b);
    }

    // use ReadWord() to read the instruction at location addr, and return true if
    // bit is 1 in the instruction, or false if 0 (bit should be in the range [0..31])
    public bool TestFlag(uint address, uint bit)
    {
        if (31 < bit) // bit not in the range [0..31])
        {
            Console.WriteLine("Cant testFlag: bit is out of range");
            return false;
        }

        uint num1 = ReadWord(address);
        num1 = BarrelShifter.lsl(num1, 31 - bit);
        num1 = BarrelShifter.lsr(num1, 31);

        if (num1 == 1)
            return true;
        else
            return false;      
    }


    // use ReadWord() to read the instruction at location addr, and set the 
    // bit whose position is specified by bit to 1 if flag is true, or 0 if flag is false.
    // places instruction back to address in RAM
    // (bit should be in the range [0..31])
    // returns 0 if able to set flag
    // returns -1 if not able to set flag
    public int SetFlag(uint address, uint bit, bool flag)
    {
        if ((0 > bit) | ((31 < bit))) // bit not in the range [0..31])
            return -1;

        uint value = ReadWord(address);
        if (value == uint.MaxValue)
            return -1;

        bool flagIsSet = TestFlag(address, bit);
        // Console.WriteLine(GetIntBinaryString(num));

        // Don't change flag if it doesn't need to
        if (flag == flagIsSet)
        {
            //Console.WriteLine(GetIntBinaryString(num));
            return -1;
        }

        // Change flag at <bit> number
        if (flagIsSet == true)
        {
            // shift a 1 to the bit counting from the right side, and XOR the number
            int IntBit = (int) bit;
            int IntValue = (int)value;
            int chFlagVal = IntValue ^ (1 << IntBit);

            uint numWFlagChanged = (uint) chFlagVal;
            WriteWord(address, numWFlagChanged);

            //Console.WriteLine(GetIntBinaryString(numWFlagChanged));
            return 0;
        }
        else // (flagIsSet == false)
        {
            // not the numbers extracted from <address>, then the resul XOR it with 1 at <bit>, then not the result to change the flag
            uint nottedValue = ~value;
            int IntNottedValue = (int)nottedValue;

            int IntBit = (int)bit;
            int IntValue = (int)value;
            int chFlagVal = ~(IntNottedValue ^ (1 << IntBit));

            uint numWFlagChanged = (uint)chFlagVal;
            WriteWord(address, numWFlagChanged);

            //Console.WriteLine(GetIntBinaryString(numWFlagChanged));
            return 0;
        }
    }



    // FUNCTION: Loads a segment to RAM
    // RETURNS: 0 if succesful
    //          -1 if not successful due to registers size
    public int loadRAMfromDataArrayBytes(ref byte[] data, ref PHE headerTable)
    {

        // if data can't fit in RAM, complain and exit program
        if (RAM.Length < headerTable.p_filesz)
        {
            Console.WriteLine("The RAM registers size is too small. Please re-run the program with a bigger RAM memmory size");
            return -1;
        }
        else if (RAM.Length < headerTable.p_vaddr)
        {
            Console.WriteLine("The start program offset is greater than the RAM size.The RAM registers size is too small. Please re-run the program with a bigger RAM memmory size.");
            return -1;
        }
        else if ((RAM.Length - headerTable.p_vaddr) < headerTable.p_filesz)
        {
            Console.WriteLine("The RAM registers size is too small. Please re-run the program with a bigger RAM memmory size");
            return -1;
        }
        else // RAM is big enoough to load data[] to RAM
        {
            uint i, n = 0;
            for (i = headerTable.p_vaddr; n < headerTable.p_filesz; i++, n++)
            {
                RAM[i] = data[n];
            }

            return 0;
        }
    }

    public void clearMemory()
    {
        Array.Clear(RAM, 0, RAM.Length);
    }

    /// RETURN: - Return 4 integer array starting at the address provided if address is multiple of 4.
    ///         - If the start address is not multiple of 4, change the address to an address 
    ///           multiple of 4 closest to the start address (In this case, the changed address is less than
    ///           the start address)
    ///           - i.e. if the provided start address is 5, then return integers starting at address 4 (<startAddress>  - <startAddress % 4>).        
    internal uint[] getFourWordsFromMemory(uint startAddress)
    {
        uint wordSizeInBytes = 4;

        // change the address to an address multiple of 4 closest to the start address
        if (startAddress % 4 != 0)
            startAddress = startAddress - (startAddress % 4);

        uint word1 = this.ReadWord(startAddress);
        uint word2 = this.ReadWord(startAddress + wordSizeInBytes);
        uint word3 = this.ReadWord(startAddress + (2 * wordSizeInBytes));
        uint word4 = this.ReadWord(startAddress + (3 * wordSizeInBytes));

        uint[] FourWordsFromMemory = { word1, word2, word3, word4 };

        return FourWordsFromMemory;
    }
} 