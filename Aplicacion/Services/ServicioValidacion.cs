using Dominio.Interfaces;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using Aplicacion.Contratos;
using System.Security.Cryptography;
using System.Text;
using Ubam.Evolution.Domain.Interfaces;

namespace Aplicacion.Servicios
{
    public class ServicioValidacion : IAccesoServicio
    {
        private readonly IUsuarioRepository _UsuarioRepositoy;
        private readonly IConfiguration _configuracion;
        private readonly IPersonaRepository _personaRepository;
        private readonly IProcedimientosRepository _procedimientosRepository;


        public ServicioValidacion(IUsuarioRepository usuarioRepository, IConfiguration configuration, IPersonaRepository personaRepository, IProcedimientosRepository procedimientosRepository)
        {
            _UsuarioRepositoy = usuarioRepository;
            _configuracion = configuration;
            _personaRepository = personaRepository;
            _procedimientosRepository = procedimientosRepository;
        }
        public async Task<bool> CrearUsuario(Usuario usuario, string txt_UsuarioContrasena)
        {

            var BucarUsuario = await _UsuarioRepositoy.GetUsuarioNombreAsync(usuario.Usuario_Nombre);
            if (BucarUsuario != null)
            {
                return false;
            }
            if (usuario.Usuario_Rol=="admin" || usuario.Usuario_Rol=="empleado" || usuario.Usuario_Rol=="cliente")
            {
                var personaId = await _personaRepository.ObtenerUltimoPersonaIdAsync();
                usuario.Usuario_ContraHash = HashContrasena(txt_UsuarioContrasena);
                usuario.Usuario_PersonaId = personaId;
                usuario.Usuario_Activo = 1;
                await _UsuarioRepositoy.CrearUsuarioAsync(usuario);
                return true;
            }
            return false;
        }

        public async Task<bool> EliminarUsuario(Usuario usuario)
        {
            var BuscarUsuario = await _UsuarioRepositoy.GetUsuarioNombreAsync(usuario.Usuario_Nombre);
            if (BuscarUsuario == null)
            {
                return false;
            }
            await _UsuarioRepositoy.EliminarUsuarioAsync(usuario.Usuario_Nombre, usuario.Usuario_Activo);
            return true;    
            
        }

        public async Task<Usuario> InciarSesion(string txt_UsuarioNombre, string txt_UsuarioContrasena)
        {
            var usuario = await _UsuarioRepositoy.GetUsuarioNombreAsync(txt_UsuarioNombre);
            if (usuario == null || !ValidacionContrasena(txt_UsuarioContrasena, usuario.Usuario_ContraHash))
            {
                return null;
            }
            return usuario;
        }


        public string HashContrasena(string txt_UsuarioContrasena)
        {
           using var hash = new HMACSHA512(Encoding.UTF8.GetBytes(_configuracion["Jwt:Key"]));
            return Convert.ToBase64String(hash.ComputeHash(Encoding.UTF8.GetBytes(txt_UsuarioContrasena)));
        }

        public bool ValidacionContrasena( string txt_UsuarioContrasena, string Stored )
        {
            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(_configuracion["Jwt:Key"]));
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(txt_UsuarioContrasena)));
            return computedHash == Stored;
        }

        public async Task<bool> CrearUsuarioCompletoAsync(Contacto contacto, Persona persona, Usuario usuario, string Contrasena)
        {

            var NombreCompleto =persona.Persona_Nombre+persona.Persona_ApellidoPaterno+persona.Persona_ApellidoMaterno.Trim().ToLower();
            var BucarUsuario = await _UsuarioRepositoy.GetUsuarioNombreAsync(usuario.Usuario_Nombre);
            var BuscarPersona = await _personaRepository.GetPersonaNombreCompletoAsync(NombreCompleto);
            if (BucarUsuario != null || BuscarPersona != null)
            {
                return false;
            }
            if (usuario.Usuario_Rol=="admin"||usuario.Usuario_Rol=="empleado"||usuario.Usuario_Rol=="cliente") { 
            var contrasena = HashContrasena(Contrasena);
            usuario.Usuario_ContraHash= contrasena;
            await _procedimientosRepository.CrearUsuarioCompletoAsync(contacto,persona,usuario);
            return true;
            }
            return false;
        }
    }
}
