using Aplicacion.Dtos;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Contratos
{
    public interface IServicioJWT
    {
        string GenerarJWT(Usuario usuario);
        void SetJwtCookie(string token);
        void DeleteJWT();
    }
}
