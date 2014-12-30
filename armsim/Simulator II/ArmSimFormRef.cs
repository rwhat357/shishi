using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel; //background worker
using System.Windows.Forms;
using System.Diagnostics; // for Trace() and Debug()
using System.Drawing;
using System.Linq;
using armsim;

// Delegate to write a character into the terminal panel
public delegate void WriteCharToTerminalDelegate(string strMessage);

// ArmSimFormRef is has reference to ArmSimForm class and its methods.
// The methods in this class are visible in all classes.
public static class ArmSimFormRef
{

    public static ArmSimForm mainwin;

    public static event WriteCharToTerminalDelegate OnWriteCharToTerminal;

    // FUNCTION:  - Run event handler ArmSimFormRef_OnWriteCharToTerminal() in ArmSimForm class which
    //              Writes a char to terminal from field charQueue in ArmSimForm class
    public static void WriteCharToTerminal(string strMessage)
    {
        ThreadSafeWriteCharToTerminal(strMessage);
    }

    // HELPER FUNCTION to WriteCharToTerminal()
    // Thread that writes a character to terminal
    private static void ThreadSafeWriteCharToTerminal(string strMessage)
    {
        if (mainwin != null && mainwin.InvokeRequired)  // we are in a different thread to the main window
        {
            mainwin.Invoke(new WriteCharToTerminalDelegate(ThreadSafeWriteCharToTerminal), new object[] { strMessage });  // call self from main thread
        }
        else
            OnWriteCharToTerminal(strMessage);
    }
}