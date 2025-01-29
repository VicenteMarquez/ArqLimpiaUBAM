using Dominio.Excepciones;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public class Usuario
    {
        public int Usuario_Id { get; set; }
        [StringLength(50)]
        public string Usuario_Nombre { get; set; } = null!;
        [StringLength(100)]
        public string Usuario_ContraHash { get; set; } = null!;
        [StringLength(20)]
        public string Usuario_Rol { get; set; }
        public int Usuario_PersonaId { get; set; }
        public int Usuario_Activo { get; set; }
        public Persona Persona { get; set; }
    }
}
