using Application.DTOs;
using Ubam.Evolution.Domain.Entities;

namespace Application.Mappers;

public class PersonMapper
{
    public Persona ToEntity(UserRequestDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Persona
        {
            Id_Persona = Guid.NewGuid(),
            Nombre_Persona = dto.FirstName.ToLower().Trim(),
            ApellidoPaterno_Persona = dto.LastNamePaternal.ToLower().Trim(),
            ApellidoMaterno_Persona = dto.LastNameMaternal.ToLower().Trim(),
            FechaDeNacimiento_Persona = dto.DateOfBirth,
            Genero_Persona = dto.Gender.ToLower().Trim(),
            Curp_Persona = dto.Curp.ToUpper().Trim()
        };
    }
}