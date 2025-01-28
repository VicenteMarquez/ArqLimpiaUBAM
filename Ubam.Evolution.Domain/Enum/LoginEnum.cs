namespace Ubam.Evolution.Domain.Enum;

public enum LoginEnum
{
    LoginSuccessful = 0,
    InvalidCredentials = 1,
    InactiveUser = 2,
    UnassignedRole = 3,
    SessionClosed = 4
}