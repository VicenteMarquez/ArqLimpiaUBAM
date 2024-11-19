using System;
using System.IO;
using System.Threading.Tasks;
using DbUp;
using DbUp.Support;
using Microsoft.Extensions.Options;

namespace MigratorDB.Main
{
    public class App
    {
        private readonly ConnectionStringCollection _settings;

        public App(IOptions<ConnectionStringCollection> settings)
        {
            _settings = settings.Value;
        }

        public async Task RunAsync(string[] args)
        {
            try
            {
                await Task.Run(() =>
                {
                    // Cadena de conexión a la base de datos tomada del archivo AppConfig.json
                    var connectionString = _settings.ConnectionStringSQLServer;

                    // Crear la base de datos si no existe
                    EnsureDatabase.For.SqlDatabase(connectionString);

                    // Rutas de los scripts
                    var pathBeforeDeployment = Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\SQLScripts\\BeforeDeployment");
                    var pathDeployment = Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\SQLScripts\\Deployment");
                    var pathPostDeployment = Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\SQLScripts\\PostDeployment");

                    Console.WriteLine("Ruta de BeforeDeployment: " + pathBeforeDeployment);
                    Console.WriteLine("Ruta de Deployment: " + pathDeployment);
                    Console.WriteLine("Ruta de PostDeployment: " + pathPostDeployment);

                    // Configurar el motor de migración de DbUp
                    var upgradeEngineBuilder = DeployChanges.To.SqlDatabase(connectionString, null)
                        // Scripts de pre-despliegue
                        .WithScriptsFromFileSystem(
                            pathBeforeDeployment,
                            script => script.EndsWith(".sql"), // Función que determina si el archivo debe ser incluido
                            new DbUp.Engine.SqlScriptOptions { ScriptType = ScriptType.RunOnce, RunGroupOrder = 0 })
                        // Scripts de despliegue principal
                        .WithScriptsFromFileSystem(
                            pathDeployment,
                            script => script.EndsWith(".sql"), // Función que determina si el archivo debe ser incluido
                            new DbUp.Engine.SqlScriptOptions { ScriptType = ScriptType.RunOnce, RunGroupOrder = 1 })
                        // Scripts de post-despliegue
                        .WithScriptsFromFileSystem(
                            pathPostDeployment,
                            script => script.EndsWith(".sql"), // Función que determina si el archivo debe ser incluido
                            new DbUp.Engine.SqlScriptOptions { ScriptType = ScriptType.RunOnce, RunGroupOrder = 2 })
                        // Por defecto, todos los scripts se ejecutan en la misma transacción
                        .WithTransactionPerScript()
                        // Registrar en la consola
                        .LogToConsole();

                    // Construir el proceso de migración
                    var upgrader = upgradeEngineBuilder.Build();

                    if (upgrader.IsUpgradeRequired())
                    {
                        var result = upgrader.PerformUpgrade();

                        // Mostrar el resultado
                        if (result.Successful)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Migración de base de datos ejecutada con éxito.");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("La migración de la base de datos falló. No se aplicaron cambios. Revisa el siguiente mensaje de error:");
                            Console.WriteLine(result.Error);
                        }
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("No se requiere actualización de la base de datos.");
                    }

                    Thread.Sleep(500);
                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error durante el proceso de migración de la base de datos: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Presiona cualquier tecla para salir...");
                Console.ReadLine();
            }
        }
    }
}
