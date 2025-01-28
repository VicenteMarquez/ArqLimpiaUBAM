using Application.DTOs;
using Ubam.Evolution.Domain.Entities;

namespace Application.Mappers;

public class UserMapper
{
    public Usuario ToEntity(UserRequestDto dto, string hashedPassword, Guid personId)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var username = $"{dto.FirstName.ToLower().Trim()}.{dto.LastNamePaternal.ToLower().Trim()}";

        return new Usuario
        {
            Id_Usuario = Guid.NewGuid(),
            Nombre_Usuario = username,
            Clave_Usuario = hashedPassword,
            Estatus_Usuario = true,
            Id_Persona = personId
        };
    }

    public UserDto ToDto(Guid userId, string userName, string role)
    {
        return new UserDto
        {
            UserId = userId,
            Username = userName,
            Role = role
        };
    }
}