using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ubam.Evolution.Domain.Events
{
    public enum EnumEvento
    {
        InicioSesion = 1,
        Registro = 2,
        InicioSesionFallido = 3,
        RegistroFallido = 4,
        InicioSesionExitoso = 5,
        RegistroPersonaFallido=6,
        RegistroPersonaExitoso = 7,
        RegistroPersonaExistente = 8,
    }
}
