using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FearEngine.Logger
{
    public enum LogPriority
    {
        NEVER = 0,
        LOW,
        MED,
        HIGH,
        EXCEPTION,
        FAILURE,
        ALWAYS,
    }

    public static class FearLog
    {
        private static LogPriority LoggingPriority{get; set;}
        private static bool IsLoggingEverything{get; set;}
        
        public static void Initialise()
        {
            LoggingPriority = LogPriority.HIGH;
        }

        public static void LogEverything( bool shouldWeLogEverything)
        {
            IsLoggingEverything = shouldWeLogEverything;
        }

        public static void Log(String message,
            LogPriority priority = LogPriority.HIGH,
            bool print = true,
            [CallerFilePath] string sourceFilePath = "",
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            int startOfFilename = sourceFilePath.LastIndexOf("\\") + 1;
            int startOfExtension = sourceFilePath.LastIndexOf(".");
            String filename = sourceFilePath.Substring(startOfFilename, startOfExtension - startOfFilename);
            String fullLog = "FearLog::";
            fullLog = fullLog + filename + "::" + memberName + "::" + sourceLineNumber + ";\t" + message;
            if(IsLoggingEverything)
            {
                Console.WriteLine(fullLog);
            }
            else
            {
                if(priority >= LoggingPriority)
                {
                    Console.WriteLine(fullLog);
                }
            }
        }
    }
}
