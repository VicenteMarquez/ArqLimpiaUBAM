using Dominio.Entidades;
namespace Aplicacion.Contratos
{
    public interface IRegistroServicio
    {
        Task RegistroMovimientos(Registro registro);
    }
}
