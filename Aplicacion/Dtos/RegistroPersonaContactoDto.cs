
using System.ComponentModel.DataAnnotations;
using Dominio.Excepciones;


namespace Aplicacion.Dtos
{
    public class RegistroPersonaContactoDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
        public string Persona_Nomb { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido paterno debe tener entre 2 y 50 caracteres.")]
        public string Persona_ApllP { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido materno debe tener entre 2 y 50 caracteres.")]
        public string Persona_ApllM { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Persona_FechNacim { get; set; }

        [Required]
        [StringLength(15,MinimumLength =15, ErrorMessage = "El teléfono personal no puede exceder los 15 caracteres.")]
        public string Persona_TelefPerson { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [StringLength(50,MinimumLength =15, ErrorMessage = "El correo electrónico no puede exceder los 100 caracteres.")]
        public string Persona_Correo { get; set; } = null!;

        [StringLength(15,MinimumLength =15, ErrorMessage = "El teléfono de contacto no puede exceder los 15 caracteres.")]
        public string Persona_TelefContacto { get; set; } = null!;
    }
}
