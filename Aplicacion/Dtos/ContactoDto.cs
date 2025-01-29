using Dominio.Excepciones;
namespace Aplicacion.Dtos
{
    public class ContactoDto
    {
        public int ContactoId { get; set; }
        public string Contacto_Telefono { get; set; } = null!;
        public string Contacto_Correo { get; set; } = null!;
        public string Contacto_TelefonoCasa { get; set; } = null!;
        public ICollection<PersonaDto> Personas { get; set; } = new List<PersonaDto>();
        public ContactoDto()
        {
            try
            {
                Contacto_Telefono = null!;
                Contacto_Correo = null!;
                Contacto_TelefonoCasa = null!;
                Personas = new List<PersonaDto>();
            }
            catch (ArgumentNullException ex) {
                ExceptionModel.EnvioArgument(ex);
            }
        }
    }
}
