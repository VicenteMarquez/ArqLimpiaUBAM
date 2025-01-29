using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ubam.Evolution.Domain.Events
{
    public  class SetEventsTimes
    {
        public  string EventsTimes(EnumEvento evento)
        {
            return evento switch
            {
                EnumEvento.InicioSesion => "Iniciando sesión...",
                EnumEvento.Registro => "Registrando usuario...",
                EnumEvento.InicioSesionFallido => "Error al iniciar sesión, verifique sus credenciales.",
                EnumEvento.RegistroFallido => "Error al registrar el usuario. Por favor, inténtelo nuevamente.",
                EnumEvento.InicioSesionExitoso => "Inicio de sesión exitoso.",
                EnumEvento.RegistroPersonaFallido => "Error al registrar la persona. Por favor, inténtelo nuevamente.",
                EnumEvento.RegistroPersonaExitoso => "Registro de persona exitoso.",
                EnumEvento.RegistroPersonaExistente => "La persona ya se encuentra registrada.",
                _ => throw new ArgumentOutOfRangeException(nameof(evento), $"El evento {evento} no está definido.")
            };
        }
    }
}
