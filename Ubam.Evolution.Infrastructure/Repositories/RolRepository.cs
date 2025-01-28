using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Ubam.Evolution.Domain.Entities;
using Ubam.Evolution.Domain.Enum;
using Ubam.Evolution.Domain.Exceptions;
using Ubam.Evolution.Domain.Interfaces;

namespace Infrastructure.Repositories;

public class RolRepository(ApplicationDbContext context) : IRolRepository
{
    public async Task<Guid> GetRolIdByRolNameAsync(string roleName)
    {
        var rolId = await context.Roles
            .Where(r => r.Nombre_Rol == roleName)
            .Select(r => r.Id_Rol)
            .FirstOrDefaultAsync();

        if (rolId == null) throw new ValidationException(ExceptionEnum.RoleNotAssigned);

        return rolId;
    }

    public async Task<Rol> AddAsync(Rol rol)
    {
        ArgumentNullException.ThrowIfNull(rol);
        await context.Roles.AddAsync(rol);
        await context.SaveChangesAsync();

        return rol;
    }
}