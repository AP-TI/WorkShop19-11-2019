using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WebShop.Swagger
{
    public class SwaggerUiRedirectMiddleware
    {
        public SwaggerUiRedirectMiddleware(RequestDelegate next, string url = null)
        {
            if (next == null) throw new ArgumentNullException(nameof(next), $"{nameof(next)} cannot be null.");
            NextDelegate = next;
            Url = String.IsNullOrWhiteSpace(url) ? "swagger/index.html" : url;
        }

        public RequestDelegate NextDelegate { get; private set; }

        public string Url { get; private set; }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path == "/")
            {
                httpContext.Response.Redirect(Url);
                return;
            }

            await NextDelegate(httpContext);
            return;
        }
    }
}
