using System;
using System.IO;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace EpiCalc.Configuration
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot GetConfiguration(string userSecretsKey = null)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            
            if (!string.IsNullOrWhiteSpace(envName))
                builder.AddJsonFile($"appsettings.{envName}.json", optional: true);
            
            if (!string.IsNullOrWhiteSpace(userSecretsKey))
                builder.AddUserSecrets(userSecretsKey);
           
            builder.AddEnvironmentVariables();
            var builtConfig = builder.Build();
            var secretClient = new SecretClient(
                new Uri($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/"),
                new DefaultAzureCredential());
            
            builder.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
            return builder.Build();
        }

    }
}
