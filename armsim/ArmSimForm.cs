using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel; //background worker
using System.Windows.Forms;
using System.Diagnostics; // for Trace() and Debug()
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

// explicit namepace because form.Dispose() won't work without it
namespace armsim 
{
    public partial class ArmSimForm: Form, Observer
    {
        Subject subject;

        Computer computer;
        Queue charsQueue = new Queue();
        string currentFileName;
        string filePath;
        uint memoryPanelNoRowsToShow;

        public ArmSimForm(Subject _subject, Computer _computer, Options _arguments)
        {
            InitializeComponent();
            this.memoryPanelNoRowsToShow = 15; // set to start showing 15 rows in memory

            // tie reference to subject and add observer to list
            this.subject = _subject;
            subject.registerObserver(this);

            // Add event handler to write a charatecter to terminal when CPU a byte in Memory from address 0x00100001
            ArmSimFormRef.OnWriteCharToTerminal += ArmSimFormRef_OnWriteCharToTerminal;

            // set object reference to computer
            this.computer = _computer;

            // initialize file name
            // load file if --load <file> argument is provided
            // run simulator if --exec and --load are provided
            // don't run simulator if --load is not provided. in this case ignore -exec if provided
            if (_arguments.fileName == null)
            {
                this.currentFileName = "none";
            }
            else // --load specified, load file
            {
                this.currentFileName = _arguments.fileName;
                this.filePath = _arguments.fileName;

                // if --exec is enabled, then run simulator until halted by CPU at the end of program
                if (_arguments.execEnabled == true)
                {
                    int loadSuccesfully = tryLoadingFileIntoMemory(); // succesfull load return 0
                    if (loadSuccesfully == 0)
                        this.runSimulator();
                    return;
                }

                // if only --load is mentioned but not --exec
                tryLoadingFileIntoMemory(); // succesfull load return 0
            }



        }

        /****************************** Observer Pattern Methods *************************************/
        // Computer notifies form on Notify_StopExecution()
        // Computer notifies form on Notify_OneCycle()
        // Memory notifies form on Notify_WriteCharToTerminal()

        // FUNCTION OVERRIDE IN OBSERVER:: update panels when the BackgroundWorker computer halts (Stop or Break)
        public void Notify_StopExecution()
        {
            updatePanelsAndResetMenuItems();
        }

        // FUNCTION OVERRIDE IN OBSERVER: Update Progress bar to show that simulator is running: 
        public void Notify_OneCycle()
        {
            //TODO
        }
        
        //FUNCTION OVERRIDE IN OBSERVER: Writes a char to terminal from Queue
        public void Notify_WriteCharToTerminal()
        {
            WriteCharToTerminal("Hello World");
        }

        /****************************** Event Handlers *************************************/

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
             if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
  
                 // get file name and update label
                this.currentFileName = System.IO.Path.GetFileName(openFileDialog1.FileName);
                
                 // try to open file
                filePath = openFileDialog1.FileName;
                //filePath = Regex.Replace(filePath, "\\","\");
                this.tryLoadingFileIntoMemory();       
            }
            
        }

        // FUNCTION: Reload the current program.
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            computer.cancelWorkerThread(); // Cancel BackgroundWorkerThread if it is running in the previous program
            this.tryLoadingFileIntoMemory();
        }

        /// HELPER FUNCTION: to loadFileToolStripMenuItem_Click() and resetToolStripMenuItem_Click() events
        /// 
        ///           Tries to load the file into RAM.
        ///           If it can't load, it displays an error dialog.
        ///           If it succesfully load, then
        ///                - reset trace counter to 1
        ///                - re-create file trace.log
        ///                - set trace ON
        ///                - update gui panels
        ///                - enable/disable toolstrip menu items
        /// RETURNS:  -1 if failed to load file into RAM
        ///            0 if loaded succesfully
        private int tryLoadingFileIntoMemory()
        {
            try
            {
                int succesfullyOpenedFile = computer.loadSegmentsIntoRAM(filePath);
                if (succesfullyOpenedFile == -1) // display a dialog error if not able to load
                {
                    displayFailedToOpenFileDialog(this.currentFileName);
                    return -1;
                }

                // get checksum and update labels
                string memChecksum = computer.getMemChecksum();
                this.checksumLabel.Text = "Checksum: " + memChecksum; // update checksum in GUI
                this.fileOpenedLabel.Text = "File opened: " + this.currentFileName; // update file name in GUI

                // reset trace 
                computer.resetTraceCounterToOne();
                computer.turnOffTraceLog();
                computer.turnOnTraceLog();
                toggleTraceOnoffToolStripMenuItem.Text = "Turn OFF trace log";

                // clear cpu list for dissembly panel
                computer.clearCPUInstructionAddressDisassembledLists();

                // clear disassembled instruction string in CPU
                computer.clearCPUDisassembledCombinedString();

                // clear terminal text and queue
                terminlaTextBox.Clear();
                charsQueue.Clear();
                
                // enable menu item availability
                updatePanelsAndResetMenuItems();
                resetToolStripMenuItem.Enabled = true;
                toggleTraceOnoffToolStripMenuItem.Enabled = true;


                return 0;
            }
            catch (Exception fileOpenExcept)
            {
                fileOpenedLabel.Text = "File opened: None";
                displayFailedToOpenFileDialog(this.currentFileName);
                Debug.WriteLine("loadFileToolStripMenuItem_Click(): " + fileOpenExcept.Message);

                return -1;
            }
        }

        /// HELPER FUNCTION: to tryLoadingFileIntoMemory()
        ///                  - Dispaly failed to open file dialog
        private void displayFailedToOpenFileDialog(string fileName)
        {
            string msg = "Cannot open file: " + fileName + ". Please make sure to open an executable file and to have enought RAM space.";
            MessageBox.Show(msg, "Error Openning Executable File",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void toggleTraceOnoffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (computer.isTraceLogEnabled())
            {
                // if enabled turn off trace log
                computer.turnOffTraceLog();
                toggleTraceOnoffToolStripMenuItem.Text = "Turn ON trace log";
            }
            else
            {
               // if disabled, turn on trace log
                computer.turnOnTraceLog();
                toggleTraceOnoffToolStripMenuItem.Text = "Turn OFF trace log";
            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.runSimulator();
        }

        /// FUNCTION RUN SIMULATOR: 
        ///             - disable run and step menu items
        ///             - enable stop menu item
        ///             - Begin simulation of execution of a program starting at PC register.
        ///             - On background thread do fetch-decode-execute 4x per second.
        ///             - update panels when
        ///                 - user hits stop/break or
        ///                 - cpu fetches 0
        ///             - -disable menu item: stop. when done executing
        private void runSimulator()
        {
            // disable run and step menu items
            runToolStripMenuItem.Enabled = false;
            singleStepToolStripMenuItem.Enabled = false;

            // enable stop menu item
            breakExecutionToolStripMenuItem.Enabled = true;

            // change simulator status to running
            simStatus.Text = "Simulator status: Running...";

            // run in a separate thread to 
            computer.runWorkerOnSeparateThread();
        }

        // Tell observers that the thread run() should be stopped.
        // Stop running the program in computer and update form panels
        private void breakExecutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cancel BackgrounWorker and update panels
            computer.cancelWorkerThread();
            
        }

        private void singleStepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            computer.step();
            updatePanelsAndResetMenuItems();
        }

        /// FUNCTION: - read textboxes from memory address and number of rows .
        ///           - show updated memory panel if inputs are correct, otherwise show an error dialog box
        ///           - updates this.memoryPanelNoRowsToShow if given correct input
        private void goButton_Click(object sender, EventArgs e)
        {
            // read the textbox numbers
            string startMem = startingMemoryTextBox.Text;
            string numRows = noRowsShowTextBox.Text;

            string pattern = @"^[0-9]\d*$"; // match only positive ints: match first digit to be 1-9, and then have any or no digits follow it
            string hexPattern = @"0[xX][0-9a-fA-F]+"; // match only hex numbers

            // Show a dialog box if user doesn't provide valid starting mem addr and number of rows to show from mem
            if (System.Text.RegularExpressions.Regex.IsMatch(startMem, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase) &&
                System.Text.RegularExpressions.Regex.IsMatch(numRows, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                // convert textbox numbers to ints
                uint startMemInt = Convert.ToUInt32(startMem); // TODO: NEED TO CHECK FOR OUT OF BOUNDS
                uint numRowsInt = Convert.ToUInt32(numRows);

                this.memoryPanelNoRowsToShow = numRowsInt; // Update the number of rows to show

                /*
                if (startMemInt % 4 != 0)
                {
                    string msg = "Please enter a memory address number multiple of 4.";
                    MessageBox.Show(msg, "Error Displaying Memory",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                */

                updateMemoryPanel(startMemInt);
            }
            else // incorrect inputs
            {
                MessageBox.Show("Please enter a valid starting memory address and number of rows to show from memory. Both numbers must be positive integers",
                                "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "This ARM7TDMI Simulator uses the ARMv4 ISA specification.";
            MessageBox.Show(msg,
                "ARM7TDMI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        //-------------------------------- Functions to update panels in the form ---------------------------------------------------------------

        /// HELPER FUNCTION: to 
        ///     - Enable menu items: Run, Single Step
        ///     - Disable menu items: Break/Stop
        ///     - Update panels: dissasembly, registers, flags, memory
        private void updatePanelsAndResetMenuItems_ForStepEvent()
        {

            // reset menu item availability
            breakExecutionToolStripMenuItem.Enabled = false;
            runToolStripMenuItem.Enabled = true;
            singleStepToolStripMenuItem.Enabled = true;

            // change simulator status to not running
            simStatus.Text = "Simulator status: stopped";

            // UPDATE PANELS
            updateRegistersPanel();
            updateFlagsPanel();

            // update memory panel
            uint startMemoryAddress = computer.getProgramCounter();
            updateMemoryPanel(startMemoryAddress);

            // update stack
            updateStackPanel();

            // update disassembly
            string str = computer.getDisassembledLastInstructionExecuted();
            disTextBox.Text += str;
            

        }


        /// HELPER FUNCTION: to 
        ///     - Enable menu items: Run, Single Step
        ///     - Disable menu items: Break/Stop
        ///     - Update panels: dissasembly, registers, flags, memory
        private void updatePanelsAndResetMenuItems()
        {

            // reset menu item availability
            breakExecutionToolStripMenuItem.Enabled = false;
            runToolStripMenuItem.Enabled = true;
            singleStepToolStripMenuItem.Enabled = true;

            // change simulator status to not running
            simStatus.Text = "Simulator status: stopped";

            // UPDATE PANELS
            updateDissaemblyPanel();
            updateRegistersPanel();
            updateFlagsPanel();

            // update memory panel
            uint startMemoryAddress = computer.getProgramCounter();
            updateMemoryPanel(startMemoryAddress);

            // update stack
            updateStackPanel();

        }

        private void updateStackPanel()
        {
            // clear rows
            stackDataGridView.Rows.Clear();

            uint numRows = 10;      // diplay top 10 instructions in stack
            uint address = 0x7000 - 4; // -4 because the stack is a full descending stack
            uint instruction = 0;

            // add rows to the stack panel
            for (uint i = 0; i <= numRows; i++)
            {
                instruction = computer.getStackInstruction(address);
                stackDataGridView.Rows.Add("0x" + address.ToString("X").PadLeft(8, '0'), instruction.ToString("X").PadLeft(8, '0'));
                address += 4;
            }
        }

        /// FUNCTION: - Add the number of rows requested to memory panel starting at <startAddress>
        ///           - Show 2 rows in memory panel previous to <startAddress> to see what instructions are before PC
        ///           - Don't show these previous 2 rows if <startAddress> is within 16 bytes from byte zero to avoid invalid 
        ///             access in array.
        /// RECEIVES CALLS FROM: -updatePanelsAndResetMenuItems()
        ///                      -goButton_Click()
        private void updateMemoryPanel(uint startAddress)
        {
            int rowToHighlight = 0; // hightlight first row to show starting address
            this.memoryDataGridView.Rows.Clear();

            uint wordSizeInBytes = 4;                    // 4 bytes = 1 word
            uint oneMemRowInBytes = wordSizeInBytes * 4; // 4 words = 1 row
            uint[] fourWordsFromMemory;

            // set up to show previous 2 rows if <startAddress> is within 16 bytes from byte zero
            if ((startAddress - (oneMemRowInBytes * 2)) >= 0)
            {
                startAddress = startAddress - (oneMemRowInBytes * 2); // show the first two rows previous to PC (program counter).
                rowToHighlight = 2;
            }

            
            // Add the number of rows requested to memory panel.
            // Add 2 rows in memory previous to PC
            // TODO: catch excess of rows requested when the RAM doesn't have a lot of memory to display that fits in the all the requested rows
            for (int i = 0; i < this.memoryPanelNoRowsToShow; i++)
            {
                fourWordsFromMemory = computer.getFourWordsFromMemory(startAddress);

                // TODO: for now if out of range in memory address, print 0. Add a handler for out of bound memory address later.
                // add one row in GridViewin order: address, hex1, hex2, hex3, h4, contents
                //string currentHexAddress = "0x" + startAddress.ToString("X").PadLeft(8, '0');
                this.memoryDataGridView.Rows.Add(          "0x" + startAddress.ToString("X").PadLeft(8, '0')         , 
                                                        fourWordsFromMemory[0].ToString("x").PadLeft(8, '0')         ,
                                                        fourWordsFromMemory[1].ToString("x").PadLeft(8, '0')         ,
                                                        fourWordsFromMemory[2].ToString("x").PadLeft(8, '0')         ,
                                                        fourWordsFromMemory[3].ToString("x").PadLeft(8, '0'));

                //increment startAddress to get the next four words from registers
                startAddress += oneMemRowInBytes;
            }

            // highlight feature for the start address row
            if (memoryDataGridView.Rows.Count > 1)
                memoryDataGridView.Rows[rowToHighlight].DefaultCellStyle.BackColor = Color.Blue;

        }

        private void updateFlagsPanel()
        {
            //clear all flag rows
            this.flagsDataGridView.Rows.Clear();

            // get flag names and values
            string[] flagNamesArray = computer.getControlFlagNames(); // get flags in this order: N, Z, C, V 
            uint[] flagsIntArray = computer.getControlFlagsIntArray(); // get flags in this order: N, Z, C, V 

            // add all flags to panel
            for (int i = 0; i < flagNamesArray.Length; i++)
                this.flagsDataGridView.Rows.Add(flagNamesArray[i], flagsIntArray[i]);

        }

        private void updateRegistersPanel()
        {
            // clear all register rows
            this.registersDataGridView.Rows.Clear();

            // get registers names values and 
            string[] registerNameArray = computer.getRegisterNames();
            uint[] registersIntArray = computer.getRegistersIntArray();

            // add all registers to panel
            int regCount = registersIntArray.Length;
            for (int i = 0; i < regCount; i++)
                this.registersDataGridView.Rows.Add(registerNameArray[i], registersIntArray[i].ToString("X").PadLeft(8, '0')); 
        }

        private void updateDissaemblyPanel()
        {
            string disStr = computer.getCPUDisassembledCombinedString();
            disTextBox.Text = disStr;
        }

      

        //-------------------------------- Functions to close the form ---------------------------------------------------------------
        
        // exit menu
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // flush and close trace and debug logs
            computer.debugLogFlush();
            computer.debugLogClose();

            computer.traceLogFlush();
            computer.traceLogClose();

            this.Close();
        }

        //-------------------------------- Functions to write to serial terminal in the form -------------------------------------------

        // This delegate enables asynchronous calls for writing a char to terminal
        delegate void ArmSimFormRef_OnWriteCharToTerminal_delegate(string text);

        // EVENT HANDLER called by Static Class ArmSimFormRef
        // This method demonstrates a pattern for making thread-safe
        // calls on a Windows Forms control. 
        //
        // If the calling thread is different from the thread that
        // created the TextBox control, this method creates a
        // ArmSimFormRef_OnWriteCharToTerminal_delegate and calls itself asynchronously using the
        // Invoke method.
        //
        // If the calling thread is the same as the thread that created
        // the terminlaTextBox control, the Text property is set directly. 
        public void ArmSimFormRef_OnWriteCharToTerminal(string strMessage)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.terminlaTextBox.InvokeRequired)
            {
                ArmSimFormRef_OnWriteCharToTerminal_delegate d = new ArmSimFormRef_OnWriteCharToTerminal_delegate(ArmSimFormRef_OnWriteCharToTerminal);
                this.Invoke(d, new object[] { strMessage });
            }
            else
            {
                //terminlaTextBox.Invoke;
                WriteCharToTerminal(strMessage);
            }

        }


        // EVENT HANDLER: Writes a charatecter to terminal when CPU a byte in Memory from address 0x00100001
        // FUNCTION: - Dequees the charQueue field by 1
        //           - Writes the dequeued character to terminal
        //           - Write 0 if queue is empty
        public void WriteCharToTerminal(string strMessage)
        {
            string chr = "";

            if (charsQueue.Count > 0)
            {
                chr = (string)charsQueue.Dequeue();
                terminlaTextBox.AppendText( chr);
            }
            else
            {
                chr = "0";
                terminlaTextBox.AppendText(chr);
            }
            //Console.WriteLine("Appended char: " + chr);

        }

        // FUNCTION: - Enqueue the characted pressed to charsQueue
        private void terminlaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string chr = e.KeyChar.ToString();
            charsQueue.Enqueue(chr);
        }

        // private void testTerminalButton_Click(object sender, EventArgs e)
        // {
        //     computer.TestArmSimFormRef_OnWriteCharToTerminal();
        // }









   
    } // end of class
    
}
