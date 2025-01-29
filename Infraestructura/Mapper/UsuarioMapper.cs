using Aplicacion.Dtos;
using Dominio.Entidades;
using Ubam.Evolution.Application.Dtos;

namespace Infraestructura.Mapper
{
    public class UsuarioMapper
    {
        public  UsuarioDto ToDtoUsuario(Usuario usuario)
        {
            return new UsuarioDto
            {
                Usuario_Nombre = usuario.Usuario_Nombre,
                Usuario_Contrasena = usuario.Usuario_ContraHash,
                Usuario_Rol = usuario.Usuario_Rol,
                Usuario_Persona = usuario.Usuario_PersonaId,
                Usuario_Activo = usuario.Usuario_Activo
            };
        }
    
        public Usuario ToEntityUsuario(UsuarioDto usuarioDto)
        {
            return new Usuario
            {
                Usuario_Nombre =usuarioDto.Usuario_Nombre,
                Usuario_ContraHash=usuarioDto.Usuario_Contrasena,
                Usuario_PersonaId=usuarioDto.Usuario_PersonaId,
                Usuario_Rol=usuarioDto.Usuario_Rol="cliente",
                Usuario_Activo =usuarioDto.Usuario_Activo,
            };
        }

        public IniciarSesionDto ToIniciarSesionDto(Usuario usuario)
        {
            return new IniciarSesionDto
            {
                Usuario_Nombre = usuario.Usuario_Nombre,
                Usuario_Contrasena = usuario.Usuario_ContraHash
            };
        }

        public Usuario ToEntityIniciarSesionDto(IniciarSesionDto iniciarSesionDto)
        {
            return new Usuario
            {
                Usuario_Nombre =iniciarSesionDto.Usuario_Nombre,
                Usuario_ContraHash=iniciarSesionDto.Usuario_Contrasena,
            };
        }

        public (Contacto,Persona,Usuario) ToEntityUsuarioCompletoDto(UsuarioCompletoDto usuarioCompletoDto)
        {
            var contacto = new Contacto
            {
                Contacto_Correo = usuarioCompletoDto.Contacto_Correo,
                Contacto_TelefonoCasa = usuarioCompletoDto.Contacto_TelefonoCasa,
                Contacto_TelefonoPersonal = usuarioCompletoDto.Contacto_TelefonoPersonal,

            };
            var persona = new Persona()
            {
                Persona_Nombre = usuarioCompletoDto.Persona_Nombre,
                Persona_ApellidoPaterno = usuarioCompletoDto.Persona_ApellidoPaterno,
                Persona_ApellidoMaterno=usuarioCompletoDto.Persona_ApellidoMaterno,
                Persona_FechaNacimiento=usuarioCompletoDto.Persona_FechaNacimiento,
            };
            var usuario = new Usuario()
            {
                Usuario_Nombre=usuarioCompletoDto.Usuario_Nombre,
                Usuario_Rol=usuarioCompletoDto.Usuario_Rol
            };

            return (contacto, persona, usuario);

        }
    }
}
