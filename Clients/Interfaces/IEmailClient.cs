using System.Threading.Tasks;

namespace Piccolo.Clients.Interfaces
{
    public interface IEmailClient
    {
        Task SendEmailAsync(string toEmailAddress, string subject, string messageText);
        Task SendActionEmailAsync(string toEmailAddress, string toName, string subject, string messageText, string actionUrl, string actionText);
    }
}