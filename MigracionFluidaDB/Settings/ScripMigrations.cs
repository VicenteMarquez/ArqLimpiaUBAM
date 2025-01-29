using DbUp;
using DbUp.Engine;
using DbUp.Support;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace MigracionFluidaDB.Settings
{
    public class ScriptMigrations
    {
        private readonly ConnectionStringCollection _settings;

        public ScriptMigrations(IOptions<ConnectionStringCollection> settings)
        {
            _settings = settings.Value;
        }

        public async Task RunAsync(string[] args)
        {
            try
            {
                await Task.Run(() =>
                {
                    var connectionString = _settings.ConnectionStringSQLServer;
                    EnsureDatabase.For.SqlDatabase(connectionString);
                    DisplayEmbeddedScripts();
                    ExecuteMigration(connectionString);
                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error inesperado durante la migración:");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.WriteLine($"Pila de llamadas: {ex.StackTrace}");
                Console.ResetColor();
            }
            finally
            {
                Console.WriteLine("\nProceso de migración finalizado. Presione cualquier tecla para continuar...");
            }
        }
        private void DisplayEmbeddedScripts()
        {
            Console.WriteLine("\n=== Scripts encontrados en el ensamblado ===");
            var scriptNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (var script in scriptNames)
            {
                Console.WriteLine($" - {script}");
            }
            Console.WriteLine("============================================\n");
        }
        private void ExecuteMigration(string connectionString)
        {
            var upgrader = ConfigureMigrationEngine(connectionString);

            if (upgrader.IsUpgradeRequired())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n=== Actualizando la base de datos ===");
                Console.ResetColor();

                var result = upgrader.PerformUpgrade();
                if (result.Successful)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Migración completada exitosamente.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error durante la migración:");
                    Console.WriteLine(result.Error);
                }
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("No se requieren actualizaciones. La base de datos ya está actualizada.");
                Console.ResetColor();
            }
        }
        private UpgradeEngine ConfigureMigrationEngine(string connectionString)
        {
            var upgradeEngineBuilder = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(
                    Assembly.GetExecutingAssembly(),
                    script => script.StartsWith("MigracionFluidaDB.SqlScripts."),
                    new SqlScriptOptions { ScriptType = ScriptType.RunOnce })
                .WithTransactionPerScript()
                .LogToConsole();
            return upgradeEngineBuilder.Build();
        }
    }
}
