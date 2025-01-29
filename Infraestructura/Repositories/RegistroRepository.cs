using Dominio.Entidades;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Repositories
{
    public class RegistroRepository : IRegistroRepository
    {
        public Task CrearRegistroAsync(Registro registro)
        {
            throw new NotImplementedException();
        }

        public Task<Registro> GetResgitroAsync(int RegistroId)
        {
            throw new NotImplementedException();
        }
    }
}
