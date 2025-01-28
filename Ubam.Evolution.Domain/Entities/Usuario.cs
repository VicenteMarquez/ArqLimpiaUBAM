using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ubam.Evolution.Domain.Entities;

[Table("Usuario")]
public class Usuario
{
    [Key] public Guid Id_Usuario { get; set; }
    public required string Nombre_Usuario { get; set; }
    public required string Clave_Usuario { get; set; }
    public required bool Estatus_Usuario { get; set; }
    public required Guid Id_Persona { get; set; }
}