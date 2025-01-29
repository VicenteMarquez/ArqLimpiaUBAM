using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ubam.Evolution.Domain.Interfaces
{
    public interface IProcedimientosRepository
    {
        Task<bool> CrearUsuarioCompletoAsync(Contacto contacto, Persona persona, Usuario usuario);
    }
}
