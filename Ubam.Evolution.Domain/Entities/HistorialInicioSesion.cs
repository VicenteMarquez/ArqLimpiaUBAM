using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ubam.Evolution.Domain.Entities;

[Table("HistorialInicioSesion")]
public class HistorialInicioSesion
{
    [Key] public Guid Id_HistorialInicioSesion { get; set; }
    public required Guid Id_Usuario { get; set; }
    public required DateTime Fecha_InicioSesion { get; set; }
    public required string Descripcion_InicioSesion { get; set; }
}