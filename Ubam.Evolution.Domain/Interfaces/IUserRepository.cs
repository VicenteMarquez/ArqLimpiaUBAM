using Ubam.Evolution.Domain.Entities;

namespace Ubam.Evolution.Domain.Interfaces;

public interface IUserRepository
{
    Task<Usuario?> GetByIdAsync(Guid id);
    Task<Usuario?> GetByUsernameAsync(string username);
    Task<List<Usuario>> GetAllAsync();
    Task<Usuario> AddAsync(Usuario usuario);
    Task UpdateAsync(Usuario usuario);
    Task UpdateStatusAsync(Guid id, bool newStatus);
    Task DeleteAsync(Guid id);
    Task<Usuario?> GetUserByUsernameAsync(string username);
}