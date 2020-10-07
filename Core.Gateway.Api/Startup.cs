using Core.Gateway.Data.Queries;
using Core.Gateway.Helper;
using Core.Gateway.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Payment.Data.Repository;
using Payment.Interface;
using Payment.Service;
using Serilog;
using System;

namespace Core.Gateway.Api
{
    public class Startup
    {
        private static IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json");
                _configuration = builder.Build();

                Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(_configuration)
                        .CreateLogger();

                Logger.InformationLog($"In Startup.ConfigureServices, Configure Services File Process Startd");
                services.AddControllers();
                services.AddControllersWithViews().AddNewtonsoftJson();

                //Ioc 
                services.AddRepository();

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(ex, $"Exception In Program.ConfigureServices. services={services.ToString()}");
            }
            finally
            {
                Logger.InformationLog($"Out Program.ConfigureServices, Configure Services File Process End");
            }


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
