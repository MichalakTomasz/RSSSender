using System;
using System.IO;

namespace RSSSender.Services
{
    public class FileLoggerService : ILoggerService
    {
        public bool IsLoggerEnabled { get; set; } = true;

        public void Log(string message)
        {
            if (!IsLoggerEnabled) return;

            using (var logStream =
                File.AppendText($"{Environment.CurrentDirectory}/appLog.log"))
            {
                var logText = $"{DateTime.Now} - {message}";
                logStream.WriteLine(logText);
            }
        }
    }
}
