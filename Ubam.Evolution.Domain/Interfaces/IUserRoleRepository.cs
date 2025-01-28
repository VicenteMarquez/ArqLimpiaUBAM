using Ubam.Evolution.Domain.Entities;

namespace Ubam.Evolution.Domain.Interfaces;

public interface IUserRoleRepository
{
    Task<string?> GetRolAsync(Guid Id);
    Task<UsuarioRol> AddAsync(UsuarioRol usuarioRol);
}