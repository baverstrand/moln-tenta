using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using EpiCalc.Configuration;
using Microsoft.ApplicationInsights.Extensibility;
using Serilog;


namespace EpiCalc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // TODO fixa key serilog som constring
            var config = ConfigurationHelper.GetConfiguration();
            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(config);
            var telemetryConfiguration = TelemetryConfiguration
                .CreateDefault();
            loggerConfiguration
                .WriteTo.ApplicationInsights(telemetryConfiguration, TelemetryConverter.Traces);
            Log.Logger = loggerConfiguration
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var builtConfig = config.Build();
                    var secretClient = new SecretClient(
                        new Uri($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/"),
                        new DefaultAzureCredential());
                    config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
                    config.AddEnvironmentVariables();
                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
