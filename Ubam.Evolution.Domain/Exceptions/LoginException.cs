using Ubam.Evolution.Domain.Enum;

namespace Ubam.Evolution.Domain.Exceptions;

public class LoginException
{
    public static string GetMessage(LoginEnum loginEnum)
    {
        switch (loginEnum)
        {
            case LoginEnum.LoginSuccessful:
                return "Inicio de sesión exitoso";
            case LoginEnum.InvalidCredentials:
                return "Inicio de sesión fallido por credenciales incorrectas";
            case LoginEnum.InactiveUser:
                return "Intento de inicio de sesión por usuario inactivo";
            case LoginEnum.UnassignedRole:
                return "Inicio de sesión fallido porque usuario no tiene rol";
            case LoginEnum.SessionClosed:
                return "Sesión cerrada";
            default:
                return "Error de sistema";
        }
    }
}