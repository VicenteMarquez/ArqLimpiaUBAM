using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class UserRequestDto
{
    public Guid Id { get; set; }

    [StringLength(100, ErrorMessage = "El primer nombre no puede tener más de 100 caracteres.")]
    public required string FirstName { get; set; }

    [StringLength(100, ErrorMessage = "El apellido paterno no puede tener más de 100 caracteres.")]
    public required string LastNamePaternal { get; set; }

    [StringLength(100, ErrorMessage = "El apellido materno no puede tener más de 100 caracteres.")]
    public required string LastNameMaternal { get; set; }

    [DataType(DataType.Date)] public required DateOnly DateOfBirth { get; set; }

    public required string Gender { get; set; }

    [StringLength(18, MinimumLength = 18, ErrorMessage = "La CURP debe tener 18 caracteres.")]
    public required string Curp { get; set; }

    [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public required string UserPassword { get; set; }

    public required string Role { get; set; }
}