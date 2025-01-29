using Dominio.Excepciones;
using System.ComponentModel.DataAnnotations;

namespace Aplicacion.Dtos
{
    public class PersonaDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Persona_Nomb { get; set; } = null!;
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Persona_ApllP {  get; set; } = null!;
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Persona_ApllM { get; set; } = null!;
        [Required]
        [DataType(DataType.Date)]
        public DateTime Persona_FechNacim { get; set; }
        [Required]
        public int Persona_ContactoId { get; set; }
        [Required]
        public int Persona_Activo { get; set; }
    }
}
