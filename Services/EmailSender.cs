using EcdsApp.Models.ViewModels;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
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
                    Host = _config.GetSection("EmailSender").GetSection("Host").Value,
                    Port = int.Parse(_config.GetSection("EmailSender").GetSection("Port").Value),
                    UserName = _config.GetSection("EmailSender").GetSection("UserName").Value,
                    Pass = _config.GetSection("EmailSender").GetSection("Password").Value,
                    Sender = _config.GetSection("EmailSender").GetSection("Sender").Value
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
