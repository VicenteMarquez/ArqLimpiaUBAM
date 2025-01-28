using Infrastructure.Data;
using Ubam.Evolution.Domain.Entities;
using Ubam.Evolution.Domain.Interfaces;

namespace Infrastructure.Repositories;

public class LoginRepository(ApplicationDbContext context) : ILoginRepository
{
    public async Task AddAsync(HistorialInicioSesion historialInicioSesion)
    {
        ArgumentNullException.ThrowIfNull(historialInicioSesion);

        await context.Set<HistorialInicioSesion>().AddAsync(historialInicioSesion);
        await context.SaveChangesAsync();
    }
}