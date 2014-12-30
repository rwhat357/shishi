using System;
using System.Windows.Forms;
using System.Collections.Generic;
using NDesk.Options;
using System.ComponentModel; //background worker
using armsim;
using armsim.Prototype; // my custom namespace
using System.Threading;
 
public class Options
{
    public string fileName;
    public uint memSize;
    public bool showTest;
    public bool execEnabled;

    public Options(string[] args)
    {
        uint defaultMemorySize = 32768; // default # of bytes
        fileName = null;
        memSize = defaultMemorySize;
        execEnabled = false;
        bool show_help = false;

        // register comand line args
        var p = new OptionSet() {
            { "l|load=", "the {NAME} of the file to load",
                v => fileName = v },
            { "m|mem=", "the {MEMORY SIZE} to load display in the RAM",
                (uint v) => memSize = v },
            { "t|test",  "check {TESTS} and exit", 
                v => showTest = v != null },
            { "h|help",  "show {HELP} message and exit",
                v => show_help = v != null },
            { "e|exec",  "switch to load {File}, run and close it once finished.",
                v => execEnabled = v != null },
        };

        List<string> extra;
        try
        {
            extra = p.Parse(args); // parse command line args and assign them to their variables
        }
        catch (OptionException e)  // if an invalid argument is entered, quit
        {
            Console.Write("arsim: ");
            Console.WriteLine(e.Message);
            Console.WriteLine("Try `armsim.exe --help' for more information.");
            Environment.Exit(0);
        }

        if (show_help) // show help section and exit the program
        {
            ShowHelp(p);
            Environment.Exit(0);
        }

        if (memSize > 1048576) //if RAM registers requested greater than 1MB = 1024*1024 = 1048576 bytes
        {
            Console.WriteLine("This application supports up to 1 MB of RAM. You requested more than 1 MB. Exiting ...");
            Environment.Exit(0);
        }
    }

    public void ShowHelp(OptionSet p)
    {
        Console.WriteLine("USAGE: armsim.exe [OPTIONS]");
        Console.WriteLine("This program simulates an arm processor by");
        Console.WriteLine("displaying the contents of the RAM and the processor.");
        Console.WriteLine();
        Console.WriteLine("OPTIONS:");
        p.WriteOptionDescriptions(Console.Out);
    }
}

class MAIN
{

    //The main entry point for the application.
    [STAThread]
    public static void Main(string[] args)
    {

        Options arguments = new Options(args);

        if (arguments.showTest) // Run Test Units and quit program if --test option was specified
        {
            Console.WriteLine("RUNNING UNIT TESTS ...");

            //TestComputer.runTests();
            //TestCPU.runtTests();
            TestMemory.runTests();

            TestRegisters.runTests();
            TestBarrelShifter.runTests();

            TestDecodeExecuteSimI.runTests();
            TestDecodeExecuteSimII.runTests();

            Console.WriteLine("All UNIT TESTS passed! Exiting ...");
            Environment.Exit(0);
        }

        // subject and observers. Observers computer and form1 add themselves to the collection of observers and hold an object reference to it.
        Subject subject = new Subject();
        Computer computer = new Computer(subject, arguments);

        // preset to launch form
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // create a form and launch it
        ArmSimForm form = new ArmSimForm(subject, computer, arguments);
        Application.Run(form);
    }
}

 