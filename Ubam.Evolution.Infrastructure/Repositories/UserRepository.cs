using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Ubam.Evolution.Domain.Entities;
using Ubam.Evolution.Domain.Enum;
using Ubam.Evolution.Domain.Exceptions;
using Ubam.Evolution.Domain.Interfaces;

namespace Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<Usuario?> GetByIdAsync(Guid id)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id_Usuario == id);
        if (user == null) throw new ValidationException(ExceptionEnum.UserNotFound);

        return user;
    }

    public async Task<Usuario?> GetByUsernameAsync(string username)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Nombre_Usuario == username);
        if (user == null) throw new ValidationException(ExceptionEnum.UserNotFound);

        return user;
    }

    public async Task<List<Usuario>> GetAllAsync()
    {
        var users = await context.Users.ToListAsync();
        if (users == null) throw new ValidationException(ExceptionEnum.UserNotFound);

        return users;
    }

    public async Task<Usuario> AddAsync(Usuario usuario)
    {
        ArgumentNullException.ThrowIfNull(usuario);
        await context.Users.AddAsync(usuario);
        await context.SaveChangesAsync();

        return usuario;
    }

    public Task UpdateAsync(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateStatusAsync(Guid id, bool newStatus)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id_Usuario == id);
        if (user == null) throw new ValidationException(ExceptionEnum.Unauthorized);

        user.Estatus_Usuario = newStatus;

        context.Users.Update(user);
        await context.SaveChangesAsync();
    }


    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Usuario?> GetUserByUsernameAsync(string username)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Nombre_Usuario == username);
        if (user == null) throw new ValidationException(ExceptionEnum.UserNotFound);

        return user;
    }
}