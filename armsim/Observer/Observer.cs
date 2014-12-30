using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace armsim
{
    // FUNCTION: Observer pattern used to communicate a computer object with
    //           the GUI.
    public interface Observer
    {
        void Notify_StopExecution();
        void Notify_OneCycle();
        void Notify_WriteCharToTerminal();
    }

    public class Subject
    {
        List<Observer> observerCollection;

        public Subject()
        {
            observerCollection = new List<Observer>();
        }

        public void registerObserver(Observer observer)
        {
            observerCollection.Add(observer);
        }

        public void unregisterObserver(Observer observer)
        {
            // TODO:
        }

        // FUNCTION: Notifies GUI to cancel
        public void notifyObservers_AboutStopExecution()
        {
            foreach (Observer ob in observerCollection)
            {
                ob.Notify_StopExecution();
            }
        }

        public void notifyObservers_AboutOneCycle()
        {
            foreach (Observer ob in observerCollection) 
            {
                ob.Notify_OneCycle();
            }
        }

        public void notifyObservers_AboutWriteCharToTerminal()
        {
            foreach (Observer ob in observerCollection)
            {
                ob.Notify_WriteCharToTerminal();
            }
        }
    }
}
