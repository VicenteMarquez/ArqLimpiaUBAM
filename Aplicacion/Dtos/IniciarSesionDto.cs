using System.ComponentModel.DataAnnotations;

namespace Aplicacion.Dtos
{
    public class IniciarSesionDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 50 caracteres.")]
        public string Usuario_Nombre { get; set; } = null!;
        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 100 caracteres.")]
        public string Usuario_Contrasena { get; set; } = null!;
    }
}
