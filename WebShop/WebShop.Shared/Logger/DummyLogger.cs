using System.Threading.Tasks;

namespace WebShop.Shared.Logger
{
    public class DummyLogger : IDummyLogger
    {
        public async Task LogAsync()
        {
            await Task.Delay(1000);
        }
    }
}
