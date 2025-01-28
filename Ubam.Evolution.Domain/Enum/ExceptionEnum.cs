namespace Ubam.Evolution.Domain.Enum;

public enum ExceptionEnum
{
    InvalidCredentials = 0,
    UserNotFound = 1,
    UserAlreadyExists = 2,
    GenericError = 3,
    InactiveUser = 4,
    InvalidInput = 5,
    Unauthorized = 6,
    RoleNotAssigned = 7,
    TokenMissing = 8,
    DataAccessException = 9,
    InvalidOperation = 10
}