using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WorklioAssessment.Helpers
{
    public class Emailer : IEmailer
    {
        private readonly EmailSettings emailSettings;

        public Emailer(EmailSettings emailSettings)
        {
            this.emailSettings = emailSettings;
        }
        public async Task SendMail(string subject, string body)
        {
            using (var client = new SmtpClient(emailSettings.Host, emailSettings.Port))
            {
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential
                {
                    UserName = emailSettings.Username,
                    Password = emailSettings.Password
                };
                await client.SendMailAsync(emailSettings.FromAddress, emailSettings.ToAddress, subject, body);
            }
        }
    }
}
