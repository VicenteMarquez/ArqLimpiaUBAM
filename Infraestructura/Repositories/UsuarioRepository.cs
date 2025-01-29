using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataContext _context;  
        public UsuarioRepository(DataContext context)  
        {
            _context = context;
        }

        public async Task<Usuario> GetUsuarioNombreAsync(string Usuario_Nombre)
        {
            return await _context.Usuarios  
                .FirstOrDefaultAsync(u => u.Usuario_Nombre == Usuario_Nombre);
        }

        public async Task CrearUsuarioAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarUsuarioAsync(string Usuario_Nombre, int Usuario_Activo)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Usuario_Nombre == Usuario_Nombre);

            if (usuario != null)
            {
                usuario.Usuario_Activo = Usuario_Activo;
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
