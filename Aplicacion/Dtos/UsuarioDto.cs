using System.ComponentModel.DataAnnotations;
namespace Aplicacion.Dtos
{
    public class UsuarioDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 50 caracteres.")]
        public string Usuario_Nombre { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 100 caracteres.")]
        public string Usuario_Contrasena { get; set; } = null!;

        [Required]
        [StringLength(20,MinimumLength =5, ErrorMessage = "El rol de usuario no puede exceder los 20 caracteres.")]
        public string Usuario_Rol { get; set; } = null!;

        public int Usuario_PersonaId { get; set; }
        [Required]
        [Range(0, 1, ErrorMessage = "Usuario_Activo debe ser 0 (inactivo) o 1 (activo).")]
        public int Usuario_Activo { get; set; }
        public int Usuario_Persona { get; set; }
    }
}
