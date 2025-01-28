using DbUp;
using Microsoft.Extensions.Configuration;

namespace MigratorDB.Main;

internal static class Program
{
    public static void Main()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        EnsureDatabase.For.SqlDatabase(connectionString);

        var upgrader = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsFromFileSystem(Path.Combine(Directory.GetCurrentDirectory(), "SQLScripts/BeforeDeployment"))
            .WithScriptsFromFileSystem(Path.Combine(Directory.GetCurrentDirectory(), "SQLScripts/Deployment"))
            .WithScriptsFromFileSystem(Path.Combine(Directory.GetCurrentDirectory(), "SQLScripts/PostDeployment"))
            .LogToConsole()
            .Build();

        // Execute the database upgrade process.
        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error);
            Console.ResetColor();
#if DEBUG
            // Pause the console in debug mode.
            Console.ReadLine();
#endif
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("¡Migración completa!");
        Console.ResetColor();
    }
}