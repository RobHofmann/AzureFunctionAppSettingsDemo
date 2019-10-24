using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using FunctionApp1.Models.Configuration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

[assembly: FunctionsStartup(typeof(FunctionApp1.Startup))]

namespace FunctionApp1
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configurationBuilder = new ConfigurationBuilder();
#if DEBUG
            configurationBuilder.AddJsonFile(System.IO.Path.Combine(
                new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.Parent.FullName,
                "appsettings.json"), false);
#else
            configurationBuilder.AddJsonFile(System.IO.Path.Combine("/home/site/wwwroot/", "appsettings.json"), false);
#endif
            var configuration = configurationBuilder.Build();

            builder.Services.AddSingleton<IConfiguration>(configuration);


            #region Configuration registering
            builder.Services.AddSingleton<CassandraConfiguration>();
            builder.Services.Configure<CassandraConfiguration>(configuration.GetSection("Cassandra"));
            builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<CassandraConfiguration>>().Value);

            builder.Services.AddSingleton<AlertingConfiguration>();
            builder.Services.Configure<AlertingConfiguration>(configuration.GetSection("Alerting"));
            builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<AlertingConfiguration>>().Value);

            builder.Services.AddSingleton<BackupConfiguration>();
            builder.Services.Configure<BackupConfiguration>(configuration.GetSection("Backup"));
            builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<BackupConfiguration>>().Value);

            builder.Services.AddSingleton<BackupArchivingConfiguration>();
            builder.Services.Configure<BackupArchivingConfiguration>(configuration.GetSection("BackupArchiving"));
            builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<BackupArchivingConfiguration>>().Value);
            #endregion Configuration registering



            builder.Services.AddHttpClient();
        }
    }
}