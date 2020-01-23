using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebShop.Data.Context;
using Swashbuckle.AspNetCore.Swagger;
using WebShop.Swagger;
using Hangfire;
using Hangfire.PostgreSql;
using WebShop.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace WebShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //TODO connection string 2: update the connection string below with the following configuration:
        //min 2 connections
        //max 90 connections
        //connection lifetime: 1min
        //command timeout: 15s
        private static readonly string _dbConnectionString = "Host=localhost;Database=postgres;Username=postgres;Password=pw;";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddEntityFrameworkNpgsql().AddDbContext<WebShopContext>(opt =>
            {
                opt.UseNpgsql($"{_dbConnectionString}"); 
                opt.UseLoggerFactory(new LoggerFactory(new[] { new ConsoleLoggerProvider((_, level) => level >= LogLevel.Information, true) }));
                //opt.UseLazyLoadingProxies();
            });
            //TODO connection string 1: update the connection string below with the following configuration:
            //min 2 connections
            //max 6 connections
            //connection lifetime: 2min
            //tip: https://www.npgsql.org/doc/connection-string-parameters.html
            services.AddHangfire(x => x.UsePostgreSqlStorage($"Host=localhost;Database=postgres;Username=postgres;Password=pw;"));

            services.AddBusinessServices();

            services.AddSwaggerGen((options) =>
            {
                options.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Webshop API",
                        Version = "v1",
                        Description = "Dummy API",
                        TermsOfService = "None",
                        Contact = new Contact()
                        {
                            Email = "Rob.Liekens@ordina.be",
                            Name = "Rob Liekens"
                        }
                    }
                    );
                options.DescribeAllEnumsAsStrings();
            });

            // TODO:
            // Configure + Add Gzip response compression
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            var hangfireServerOpts = new BackgroundJobServerOptions { WorkerCount = 4 };
            app.UseHangfireServer(hangfireServerOpts);

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            });

            app.UseMiddleware<SwaggerUiRedirectMiddleware>();

            app.UseMvc(routes => { });
        }
    }
}
