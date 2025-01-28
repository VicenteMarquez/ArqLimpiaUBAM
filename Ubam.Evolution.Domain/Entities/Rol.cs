using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ubam.Evolution.Domain.Entities;

[Table("Rol")]
public class Rol
{
    [Key] public Guid Id_Rol { get; set; }
    public required string Nombre_Rol { get; set; }
}