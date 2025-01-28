using Ubam.Evolution.Domain.Entities;

namespace Ubam.Evolution.Domain.Interfaces;

public interface ILoginRepository
{
    Task AddAsync(HistorialInicioSesion history);
}