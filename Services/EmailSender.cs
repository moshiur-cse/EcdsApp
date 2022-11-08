using DRIPWebApp.Data;
using EcdsApp.Models.ViewModels;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;

namespace EcdsApp.Services
{

    public class EmailSender : IEmailSender

    {
        private readonly IConfiguration _config;
        public EmailSender(IConfiguration config)
        {
            _config = config;
        }
        public async Task<bool> SendEmailAsync(EmailModel emodel)
        {
            try
            {
                var email = new MimeMessage();
                //email.From.Add(MailboxAddress.Parse(emodel.From));
                email.To.Add(MailboxAddress.Parse(emodel.To));
                email.Subject = emodel.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = emodel.Msg };

                // send email
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                EmailConfig emconfig = new EmailConfig()
                {
                    Host = Credentials.Host,
                    Port = Credentials.Port,
                    UserName = Credentials.UserName,
                    Pass = Credentials.Password,
                    Sender = Credentials.EmailSender
                };
                email.From.Add(MailboxAddress.Parse(emconfig.Sender));
                smtp.Connect(emconfig.Host, emconfig.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(emconfig.UserName, emconfig.Pass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }


}
