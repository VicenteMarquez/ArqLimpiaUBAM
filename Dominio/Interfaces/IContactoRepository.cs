using Dominio.Entidades;
namespace Dominio.Interfaces
{
    public interface IContactoRepository
    {
        Task<Contacto> GetContactoAsync(int ContactoId);
        Task<int> CrearContactoAsync(Contacto contacto);
        Task ActualizarContactoAsync(Contacto contacto);
    }
}
