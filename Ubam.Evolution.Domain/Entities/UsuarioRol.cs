using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ubam.Evolution.Domain.Entities;

[Table("UsuarioRol")]
public class UsuarioRol
{
    [Key] public Guid Id_UsuarioRol { get; set; }
    public Guid Id_Usuario { get; set; }
    public Guid Id_Rol { get; set; }
}