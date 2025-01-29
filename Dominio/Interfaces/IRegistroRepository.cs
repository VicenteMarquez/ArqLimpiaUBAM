using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IRegistroRepository
    {
        Task<Registro> GetResgitroAsync(int RegistroId);
        Task CrearRegistroAsync(Registro registro);
    }
}
