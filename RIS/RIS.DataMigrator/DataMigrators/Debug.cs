using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace RIS.RISService.DataMigrators
{
    class Debug
    {
        private EventLog eventLog;
        private Debug() 
        {
            isEnabled = true;
            eventLog = null;
        }
        private static Debug instance = null;

        private bool isEnabled;
        public static Debug Instance
        {
            get
            {
                if (instance == null) instance = new Debug();
                return instance;
            }
        }
        public void Enable()
        {
            isEnabled = true;
        }
        public void Disable()
        {
            isEnabled = false;
        }

        public EventLog EventLog
        {
            set
            {
                this.eventLog = value;
            }
        }

        public void Log(string message)
        {
            if (isEnabled == true)
            {
                if (eventLog != null)
                    eventLog.WriteEntry(message);
                else
                    Console.WriteLine(message);
            }
        }
    }
}
