using System.Threading.Tasks;

namespace WebShop.Shared.Mailer
{
    public interface IDummyMailer
    {
        Task MailAsync();
    }
}