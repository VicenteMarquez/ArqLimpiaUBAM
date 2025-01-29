using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSeguro.Settings
{
        class Program
        {
        static async Task Main(string[] args)
        {
            var services = ConfigureServices();
            var ServiceProvider = services.BuildServiceProvider();
            await ServiceProvider.GetService<ScripMigrations>().RunAsync(args);
        }
            public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
            }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            var config = LoadConfiguration();
            services.AddSingleton(config);
            services.Configure<ConnectionStringCollection>(options => config.GetSection($"CollectionConnectionStrings").Bind(options));
            services.AddSingleton<ScripMigrations>();
            return services;
        }




        }
}
