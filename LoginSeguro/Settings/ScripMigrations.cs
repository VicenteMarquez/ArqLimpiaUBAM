using DbUp;
using DbUp.Engine;
using DbUp.Support;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace LoginSeguro.Settings
{
    public class ScripMigrations
    {
        private readonly ConnectionStringCollection _settings;
        public ScripMigrations(IOptions<ConnectionStringCollection> settings) { _settings = settings.Value;}

        public async Task RunAsync(string[] args)
        {
            try
            {
                await Task.Run(() => {
                    var connectionString = _settings.ConnectionStringSqlServer;
                    EnsureDatabase.For.SqlDatabase(connectionString);
                    var upgradeEngineBuilder = DeployChanges.To.SqlDatabase(connectionString, null)
                                            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), x => x.StartsWith($"LognSeguro.LoginSeguro.SqlScripts.DevelopmentScript_Preview."), new SqlScriptOptions { ScriptType = ScriptType.RunOnce, RunGroupOrder = 0 })
                                            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), x => x.StartsWith($"LognSeguro.LoginSeguro.SqlScripts.DevelopmentScript."), new SqlScriptOptions { ScriptType = ScriptType.RunOnce, RunGroupOrder = 1 })
                                            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), x => x.StartsWith($"LognSeguro.LoginSeguro.SqlScripts.DevelopmentScript_Before."), new SqlScriptOptions { ScriptType = ScriptType.RunAlways, RunGroupOrder = 2 })
                                            .LogToConsole();

                    Console.WriteLine($"Actualizando la dase de datos....");
                    var upgrader = upgradeEngineBuilder.Build();
                    if (upgrader.IsUpgradeRequired())
                    {
                        var result = upgrader.PerformUpgrade();
                        if (result.Successful)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Ejecución satisfactoria la migración de la base de datos");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"Error Migracion Fluida For All-0 en la migración de la base de la base de datos, Revise el scripts de la MIgración fluida.");
                            Console.WriteLine(result.Error);
                        }
                        Console.ResetColor();
                    }
                    Thread.Sleep(500);
                }).ConfigureAwait(false);
            }
            catch (Exception oEx)
            {
                Console.WriteLine($"Ocurrio algo extraño en la Migracion. Error: MIgrations Fluida For All-1 {oEx.Message.Trim()}.");
            }
            finally
            {
                Console.WriteLine("Migración Exitosa... Ingrese cualquier tecla.");
            }
        }
    }
}
