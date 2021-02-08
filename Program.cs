using System;
using FluentMigrator.Runner;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore;
using Bakery.DAO;

namespace Bakery
{
    public class Program
    {
        public static IConfigurationRoot config;
        public static void Main(string[] args)
        {
            config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: false)
                .AddCommandLine(args).AddEnvironmentVariables().Build();

            var host = CreateHostBuilder(args);
            using (var scope = host.Services.CreateScope())
            {
                #region Migrations
                var serviceProvider = CreateServices();
                using(var migrationScope = serviceProvider.CreateScope())
                {
                    UpdateDatabase(migrationScope.ServiceProvider);
                }
                #endregion
            }

            host.Run();
        }

        public static IWebHost CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseUrls(config["AppUrl"]).UseConfiguration(config).UseContentRoot(Directory.GetCurrentDirectory()).UseStartup<Startup>().CaptureStartupErrors(true).Build();

        #region Migrations
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection().AddFluentMigratorCore().ConfigureRunner(rb =>
            rb.AddSqlServer2016()
            .WithGlobalConnectionString(config.GetConnectionString("DefaultConnection"))
            .ScanIn(typeof(Migrations._20210109155900_FirstMigration).Assembly).For.Migrations()).AddLogging(lb => lb.AddFluentMigratorConsole()).BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
        #endregion
    }
}
