using System;
using System.Collections.Generic;
using System.Text;

namespace RIS.RISVoiceControl.Utils
{
    public static class Constants
    {
        public static string ClientDirectory = @"C:\RIS\Findings";
        public static string LogDirectory = @"C:\RIS\Log";
        public static string LogFileName = "AudioControl.log";
        public static double FileCreationLimitInHours = 24; 

        public static class Messages
        {
            public static class Error
            {
                public static string ServerNotAvailable = "Unable to connect to the server.";
                public static string ServerReturnedError = "Server returned error.";
                public static string UnableToReadFile = "Unable to read recording file.";
                public static string UnableToWriteFile = "Unable to write recording file.";
            }
            public static class Information
            {
                public static string Recording = "Recording...";
                public static string Playing = "Playing...";
                public static string Stopped = "Stopped.";
                public static string Paused = "Paused";
                public static string Saved = "File Saved to server";
            }
        }
    }
}
