using System.Threading.Tasks;

namespace PMS.Web.UI.Core.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
