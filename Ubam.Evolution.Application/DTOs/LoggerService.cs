using System;
using Ubam.Evolution.Application.Contracts;

namespace Ubam.Evolution.Application.Services
{
    public class LoggerService : ILoggerService
    {
        public void Log(string message)
        {
            // Registra el mensaje en la consola
            Console.WriteLine($"[{DateTime.Now}] {message}");
        }
    }
}
