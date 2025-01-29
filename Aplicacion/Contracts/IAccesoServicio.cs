using Dominio.Entidades;
using Ubam.Evolution.Application.Dtos;
namespace Aplicacion.Contratos
{
    public interface IAccesoServicio
    {
        Task<Usuario> InciarSesion(string txt_UsuarioNombre, string txt_UsuarioContrasena);
        Task<bool> CrearUsuario(Usuario usuario, string txt_UsuarioContrasena);
        Task<bool> EliminarUsuario(Usuario usuario);
        Task<bool> CrearUsuarioCompletoAsync(Contacto contacto,Persona persona,Usuario usuario, string Contrasena);
    }
}
