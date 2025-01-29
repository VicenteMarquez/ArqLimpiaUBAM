using Aplicacion.Dtos;
using Dominio.Entidades;

namespace Infraestructura.Mapper
{
    public class PersonaMapper
    {
        public PersonaDto ToDto(Persona persona)
        {
            return new PersonaDto
            {
                Persona_Nomb = persona.Persona_Nombre,
                Persona_ApllP = persona.Persona_ApellidoPaterno,
                Persona_ApllM = persona.Persona_ApellidoMaterno,
                Persona_FechNacim = persona.Persona_FechaNacimiento,
                Persona_ContactoId = persona.Persona_ContactoId,
                Persona_Activo = persona.Persona_Activo
            };
        }

        public  Persona ToEntity(PersonaDto dto)
        {
            return new Persona
            {
                Persona_Nombre = dto.Persona_Nomb,
                Persona_ApellidoPaterno = dto.Persona_ApllP,
                Persona_ApellidoMaterno = dto.Persona_ApllM,
                Persona_FechaNacimiento = dto.Persona_FechNacim,
                Persona_ContactoId = dto.Persona_ContactoId,
                Persona_Activo = dto.Persona_Activo
            };
        }


        public (Persona, Contacto) ToEntityRegistroPersonalContactoDto(RegistroPersonaContactoDto dto)
        {
            
            var contacto = new Contacto
            {
                Contacto_TelefonoPersonal = dto.Persona_TelefPerson,
                Contacto_Correo = dto.Persona_Correo,
                Contacto_TelefonoCasa = dto.Persona_TelefContacto
            };
            var persona = new Persona
            {
                Persona_Nombre = dto.Persona_Nomb,
                Persona_ApellidoPaterno = dto.Persona_ApllP,
                Persona_ApellidoMaterno = dto.Persona_ApllM,
                Persona_FechaNacimiento = dto.Persona_FechNacim,
                Contactos = contacto
            };

            return (persona, contacto);
        }
    }
}


