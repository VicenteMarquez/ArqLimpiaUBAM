using Application.DTOs;

namespace Application.Contracts;

public interface IJwtService
{
    string GenerateJwtToken(UserDto user);
    UserDto ValidateToken(string token);
}