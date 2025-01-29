using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuarioNombreAsync(string Usuario_Nombre);
        Task CrearUsuarioAsync(Usuario usuario);
        Task ActualizarUsuarioAsync(Usuario usuario);
        Task EliminarUsuarioAsync(string Usuario_Nombre,int Usuario_Activo);
    }
}
