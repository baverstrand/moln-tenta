using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using EpiCalc.Configuration;
using EpiCalc.Models;
using EpiCalc.Service;
using Microsoft.Extensions.Options;
using Serilog;

namespace EpiCalc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Serilog tillägg
            services.AddSingleton(Log.Logger);
            services.AddControllersWithViews();

            services.AddControllers();

            var connectionString = GetConString();
            var dbSettings = new EpiCalcDbSettings() {CosmosDbConnectionString = connectionString};
            Configuration.GetSection("EpiCalcDbSettings").Bind(dbSettings);

            services.AddSingleton<IEpiCalcDbSettings>(dbSettings);

            services.Configure<ApiSettings>(
                Configuration.GetSection("ApiSettings"));

            services.AddScoped<ResultService>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }

        public static string GetConString()
        {
            var sClient = new SecretClient(new Uri("https://epicalckeys.vault.azure.net/"), new DefaultAzureCredential());
            KeyVaultSecret secret =  sClient.GetSecret("EpiCalcDb");
            return (secret.Value);
        }
    }
}
