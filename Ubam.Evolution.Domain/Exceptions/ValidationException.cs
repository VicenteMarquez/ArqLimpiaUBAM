using Ubam.Evolution.Domain.Enum;

namespace Ubam.Evolution.Domain.Exceptions;

public class ValidationException(ExceptionEnum exceptionType) : Exception(GetMessage(exceptionType))
{
    private static string GetMessage(ExceptionEnum exceptionType)
    {
        switch (exceptionType)
        {
            case ExceptionEnum.InvalidCredentials:
                return "Credenciales inválidas.";
            case ExceptionEnum.UserNotFound:
                return "El usuario no fue encontrado.";
            case ExceptionEnum.UserAlreadyExists:
                return "El usuario ya existe.";
            case ExceptionEnum.GenericError:
                return "Error al procesar la solicitud.";
            case ExceptionEnum.InactiveUser:
                return "Acceso denegado (Usuario inactivo).";
            case ExceptionEnum.InvalidInput:
                return "Datos de entrada inválidos.";
            case ExceptionEnum.Unauthorized:
                return "No autorizado.";
            case ExceptionEnum.RoleNotAssigned:
                return "El usuario no tiene un rol asignado.";
            case ExceptionEnum.TokenMissing:
                return "No hay token presente en la solicitud.";
            case ExceptionEnum.DataAccessException:
                return "Error al obtener los datos.";
            case ExceptionEnum.InvalidOperation:
                return "Token invalido: No hay datos";
            default:
                return "Error desconocido.";
        }
    }
}