using EcdsApp.Models.ViewModels;
using System.Threading.Tasks;

namespace EcdsApp.Services
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(EmailModel emodel);
    }
}