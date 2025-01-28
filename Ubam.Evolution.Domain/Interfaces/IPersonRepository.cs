using Ubam.Evolution.Domain.Entities;

namespace Ubam.Evolution.Domain.Interfaces;

public interface IPersonRepository
{
    Task<Persona?> GetByIdAsync(Guid id);
    Task<Persona?> GetByCurpAsync(string curp);
    Task<List<Persona>> GetAllAsync();
    Task<Persona> AddAsync(Persona persona);
    Task UpdateAsync(Persona persona);
    Task DeleteAsync(Guid id);
    Task<List<Persona>> GetDocentesAsync();
    Task<List<Persona>> GetAlumnosAsync();
}