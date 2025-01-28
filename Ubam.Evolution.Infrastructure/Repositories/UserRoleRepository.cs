using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Ubam.Evolution.Domain.Entities;
using Ubam.Evolution.Domain.Enum;
using Ubam.Evolution.Domain.Exceptions;
using Ubam.Evolution.Domain.Interfaces;

namespace Infrastructure.Repositories;

public class UserRoleRepository(ApplicationDbContext context) : IUserRoleRepository
{
    public async Task<string?> GetRolAsync(Guid userId)
    {
        var roleName = await context.UserRoles
            .Where(ur => ur.Id_Usuario == userId)
            .Join(context.Roles, ur => ur.Id_Rol, r => r.Id_Rol, (ur, r) => r.Nombre_Rol)
            .FirstOrDefaultAsync();

        if (roleName == null) throw new ValidationException(ExceptionEnum.DataAccessException);

        return roleName;
    }

    public async Task<UsuarioRol> AddAsync(UsuarioRol usuarioRol)
    {
        ArgumentNullException.ThrowIfNull(usuarioRol);
        await context.UserRoles.AddAsync(usuarioRol);
        await context.SaveChangesAsync();

        return usuarioRol;
    }
}