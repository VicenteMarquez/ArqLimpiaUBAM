using Ubam.Evolution.Domain.Entities;

namespace Application.Mappers;

public class UserRoleMapper
{
    public UsuarioRol ToEntity(Guid userId, Guid roleId)
    {
        return new UsuarioRol
        {
            Id_UsuarioRol = Guid.NewGuid(),
            Id_Usuario = userId,
            Id_Rol = roleId
        };
    }
}