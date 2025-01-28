namespace Ubam.Evolution.Domain.Interfaces;

public interface IRolRepository
{
    Task<Guid> GetRolIdByRolNameAsync(string rolName);
}