using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WorklioAssessment.Helpers
{
    public interface IMyLogger
{
    Task LogMessage(LogLevel logLevel, string message);
}
}
