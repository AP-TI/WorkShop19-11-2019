using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var webhost = CreateWebHostBuilder(args).Build())
            {
                webhost.Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                // TODO
                // Configure Kestrel to listen at localhost port 5001
                // Use Https
                // Enable both Http1 and Http2 protocols
                .UseStartup<Startup>();
    }
}
