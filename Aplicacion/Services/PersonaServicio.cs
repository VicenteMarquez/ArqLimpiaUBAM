using Aplicacion.Contratos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Excepciones;

namespace Aplicacion.Servicios
{
    public class PersonaServicio :IPersonaServicio
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IContactoRepository _contactoRepository;

        public PersonaServicio(IPersonaRepository personaRepository, IContactoRepository contactoRepository)
        {
            _personaRepository = personaRepository;
            _contactoRepository = contactoRepository;
        }
        public async Task ActualizarPersona(Persona persona, string NombreCompleto)
        {
            var BuscarPersona = _personaRepository.GetPersonaNombreCompletoAsync(NombreCompleto);
            if (BuscarPersona != null)
            {
                await _personaRepository.ActualizarPersonaAsync(persona);
            }
        }
         public async Task<bool> CrearPersona(Persona persona, Contacto contacto)
         {

             var NombreCompleto = persona.Persona_Nombre+ persona.Persona_ApellidoPaterno+ persona.Persona_ApellidoMaterno.Trim().ToLower();
             var BuscarPersona = await _personaRepository.GetPersonaNombreCompletoAsync(NombreCompleto);
             if (BuscarPersona != null)
             {
                 return false;
             }
             try
             {
             var contactoId = await _contactoRepository.CrearContactoAsync(contacto);

             persona.Persona_ContactoId = contactoId;
             persona.Persona_Activo = 1;
             await _personaRepository.CrearPersonaAsync(persona);
             return true;
             }
             catch (ArgumentException ex)
             {
                 ExceptionModel.EnvioArgument(ex);
                 return false;
             }
         }

        public async Task EliminarPersona(int Persona_Activo, string NombreCompleto)
        {
            var BuscarPersona = _personaRepository.GetPersonaNombreCompletoAsync(NombreCompleto);
            if (BuscarPersona != null)
            {
                await _personaRepository.EliminarPersonaAsync(BuscarPersona.Id, Persona_Activo);
            }
        }
    }
}
