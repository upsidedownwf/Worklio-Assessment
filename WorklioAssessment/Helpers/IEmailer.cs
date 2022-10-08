using System.Threading.Tasks;

namespace WorklioAssessment.Helpers
{
    public interface IEmailer
{
    Task SendMail(string subject, string body);
}
}
