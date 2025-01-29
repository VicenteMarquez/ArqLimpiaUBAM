using Dapper;
using Dominio.Entidades;
using Dominio.Excepciones;
using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Ubam.Evolution.Domain.Interfaces;

namespace Ubam.Evolution.Infrastructure.Repositories
{
    public class ProcedimientosRepository : IProcedimientosRepository
    {

        private readonly DataContext _dataContext;
        public ProcedimientosRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> CrearUsuarioCompletoAsync(Contacto contacto, Persona persona, Usuario usuario)
        {
            using var connection = _dataContext.Database.GetDbConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@Persona_Nombre", persona.Persona_Nombre);
            parameters.Add("@Persona_ApellidoPaterno", persona.Persona_ApellidoPaterno);
            parameters.Add("@Persona_ApellidoMaterno", persona.Persona_ApellidoMaterno);
            parameters.Add("@Persona_FechaNacimiento", persona.Persona_FechaNacimiento);
            parameters.Add("@Contacto_TelefonoPersonal", contacto.Contacto_TelefonoPersonal);
            parameters.Add("@Contacto_Correo", contacto.Contacto_Correo);
            parameters.Add("@Contacto_TelefonoCasa", contacto.Contacto_TelefonoCasa);
            parameters.Add("@Usuario_Nombre", usuario.Usuario_Nombre);
            parameters.Add("@Usuario_Contrasena", usuario.Usuario_ContraHash);
            parameters.Add("@Usuario_Rol", usuario.Usuario_Rol);

            try
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("sp_RegistroUsuarioCompleto", parameters, commandType: CommandType.StoredProcedure);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionModel.EnvioArgument(ex);
                return false;
                
            }
        }
    }
}
