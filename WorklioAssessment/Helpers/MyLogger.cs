using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WorklioAssessment.Helpers
{
    public class MyLogger : IMyLogger
    {
        public readonly string path = "c:/worklioassessment/logs";
        public async Task LogMessage(LogLevel logLevel, string message)
        {
            Directory.CreateDirectory(path);
            string date = DateTime.Today.ToString("dd-MM-yyyy");
            string logmessage = $"{DateTime.Now:dd/MM/yyyy hh:mm:ss tt}: {logLevel}: {message}\n";
            await File.AppendAllTextAsync($"{path}/log-{date}.txt", logmessage);
        }
    }
}
