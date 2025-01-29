using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IPersonaRepository
    {
        Task<Persona> GetPersonaNombreCompletoAsync(string NombreCompleto);
        Task CrearPersonaAsync(Persona persona);
        Task ActualizarPersonaAsync(Persona persona);
        Task EliminarPersonaAsync(int PersonaId,int Persona_Activo);
        Task<int> ObtenerUltimoPersonaIdAsync();
    }
}
