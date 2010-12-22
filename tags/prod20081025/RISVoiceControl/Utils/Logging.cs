using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RIS.RISVoiceControl.Utils
{
    public class Logging
    {
        bool Debug = false;
        private static Logging instance = null;
        StreamWriter logWriter = null;
        
        public static Logging Instance
        {
            get
            {
                if (instance == null)
                    instance = new Logging();
                return instance;
            }
        }

        private Logging()
        {
            if (Debug == false) return;
            if (!Directory.Exists(Constants.LogDirectory))
                Directory.CreateDirectory(Constants.LogDirectory);
            /*if (logWriter == null)
                logWriter = new StreamWriter(Constants.LogDirectory + "\\" + Constants.LogFileName);*/
        }

        public void WriteLine(string line)
        {
            if (Debug == false) return;
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append(DateTime.Now.ToString());
            sb.Append("]");
            sb.Append(line);
            logWriter = new StreamWriter(Constants.LogDirectory + "\\" + Constants.LogFileName,true);
            logWriter.WriteLine(sb.ToString());
            logWriter.Flush();
            logWriter.Close();
        }        
    }
}
