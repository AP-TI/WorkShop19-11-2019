using System.Threading.Tasks;

namespace WebShop.Shared.Mailer
{
    public class DummyMailer : IDummyMailer
    {
        public async Task MailAsync()
        {
            await Task.Delay(3000);
        }
    }
}
