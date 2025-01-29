using Aplicacion.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class LoggerServicio : ILoggerServicio
    {
        public void Log(string message)
        {
            Console.WriteLine($"[{DateTime.Now}] {message}");
            
        
        }
    }
}
