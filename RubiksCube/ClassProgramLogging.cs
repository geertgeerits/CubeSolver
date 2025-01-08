using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace RubiksCube
{
    internal sealed class ClassProgramLogging
    {
        public static void LogExecutedLineTest()
        {
            LogExecutedLine();
        }

        /// <summary>
        /// Log executed lines
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="lineNumber"></param>
        /// <param name="memberName"></param>
        public static void LogExecutedLine([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            StackTrace stackTrace = new StackTrace(true);
            StackFrame? frame = stackTrace.GetFrame(1);  // Get the previous frame
            
            if (frame != null)
            {
                int frameLineNumber = frame.GetFileLineNumber();
                string? frameMethodName = frame.GetMethod()?.Name;
                string? frameFileName = frame.GetFileName();

                Debug.WriteLine($"Executed line: {lineNumber} in method: {memberName} of file: {filePath}");
                Debug.WriteLine($"Stack trace line: {frameLineNumber} in method: {frameMethodName} of file: {frameFileName}");
            }
        }
    }
}
