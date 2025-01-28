using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ubam.Evolution.Domain.Entities;

[Table("Persona")]
public class Persona
{
    [Key] public Guid Id_Persona { get; set; }
    public required string Nombre_Persona { get; set; }
    public required string ApellidoPaterno_Persona { get; set; }
    public required string ApellidoMaterno_Persona { get; set; }
    public required DateOnly FechaDeNacimiento_Persona { get; set; }
    public required string Genero_Persona { get; set; }
    public required string Curp_Persona { get; set; }
}