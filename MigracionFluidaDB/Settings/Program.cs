using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MigracionFluidaDB.Settings
{
        class Program
        {
            static async Task Main(string[] args)
            {
                var services = ConfigureServices();
                var ServiceProvider = services.BuildServiceProvider();
                await ServiceProvider.GetService<ScriptMigrations>().RunAsync(args);
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
                services.AddSingleton<ScriptMigrations>();
                return services;
            }
        }
}
