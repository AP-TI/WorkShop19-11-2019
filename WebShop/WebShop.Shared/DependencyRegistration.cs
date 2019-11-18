using Microsoft.Extensions.DependencyInjection;
using WebShop.Shared.Logger;
using WebShop.Shared.Mailer;

namespace WebShop.Shared
{
    public static class DependencyRegistration
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddTransient<IDummyLogger, DummyLogger>();
            services.AddTransient<IDummyMailer, DummyMailer>();
        }
    }
}
