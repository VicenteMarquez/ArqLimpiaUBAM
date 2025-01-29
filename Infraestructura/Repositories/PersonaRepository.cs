
using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly DataContext _context;

        public PersonaRepository(DataContext context)
        {
            _context = context;
        }

        public async Task ActualizarPersonaAsync(Persona persona)
        {
            _context.Personas.Update(persona);
            await _context.SaveChangesAsync();
        }

        public async Task CrearPersonaAsync(Persona persona)
        {
            await _context.Personas.AddAsync(persona);
            await _context.SaveChangesAsync();
          
        }

        public async Task EliminarPersonaAsync(int PersonaId, int Persona_Activo)
        {
           var persona = await _context.Personas
                .FirstOrDefaultAsync(p => p.Persona_Id == PersonaId);
            if (persona != null)
            {
                persona.Persona_Activo = Persona_Activo;
                _context.Personas.Update(persona);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Persona> GetPersonaNombreCompletoAsync(string NombreCompleto)
        {
            var formattedFullName = NombreCompleto.Trim().ToLower();

            return await _context.Personas
                .FirstOrDefaultAsync(p =>
                    (p.Persona_Nombre.Trim().ToLower() + " " +
                     p.Persona_ApellidoPaterno.Trim().ToLower() + " " +
                     p.Persona_ApellidoMaterno.Trim().ToLower()) == formattedFullName);
        }
        public async Task<int> ObtenerUltimoPersonaIdAsync()
        {
            var id = await _context.Personas
                .OrderByDescending(p => p.Persona_Id)
                .Select(p => p.Persona_Id)
                .FirstOrDefaultAsync();
            return id;
        }


    }

}
