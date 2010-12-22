using System;
using System.Collections.Generic;
using System.Text;

namespace RIS.RISVoiceControl.Utils
{
    public class VoiceControlExcpetion : System.Exception
    {
        public VoiceControlExcpetion(string message)
            : base(message)
        {
        }
    }
}
