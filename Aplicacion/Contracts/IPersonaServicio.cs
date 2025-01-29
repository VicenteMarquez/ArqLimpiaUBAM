using Aplicacion.Dtos;
using Dominio.Entidades;
namespace Aplicacion.Contratos
{
    public interface IPersonaServicio
    {
        Task<bool> CrearPersona(Persona persona, Contacto contacto);
        Task ActualizarPersona(Persona persona, string NombreCompleto);
        Task EliminarPersona(int Persona_Activo, string NombreCompleto);
    }
}
