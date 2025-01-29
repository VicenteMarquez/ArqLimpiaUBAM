using System.ComponentModel.DataAnnotations;

namespace Ubam.Evolution.Application.Dtos
{
    public class UsuarioCompletoDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
        public string Persona_Nombre { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido paterno debe tener entre 2 y 50 caracteres.")]
        public string Persona_ApellidoPaterno { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido materno debe tener entre 2 y 50 caracteres.")]
        public string Persona_ApellidoMaterno { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Persona_FechaNacimiento { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "El correo electrónico no puede exceder los 100 caracteres.")]
        public string Contacto_Correo { get; set; } = null!;

        [Required]
        [StringLength(15, MinimumLength = 15, ErrorMessage = " El teléfono personal no puede exceder los 15 caracteres.")]
        public string Contacto_TelefonoCasa { get; set; } = null!;

        [Required]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "El teléfono de contacto no puede exceder los 15 caracteres.")]
        public string Contacto_TelefonoPersonal { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 50 caracteres.")]
        public string Usuario_Nombre { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 100 caracteres.")]
        public string Usuario_Contrasena { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "El rol de usuario no puede exceder los 20 caracteres.")]
        public string Usuario_Rol { get; set; } = null!;

        [Required]
        [Range(0, 1, ErrorMessage = "Usuario_Activo debe ser 0 (inactivo) o 1 (activo).")]
        public int Usuario_Activo { get; set; }
    }
}
    
