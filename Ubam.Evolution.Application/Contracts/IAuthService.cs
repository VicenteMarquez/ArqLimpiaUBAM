using Application.DTOs;

namespace Application.Contracts;

public interface IAuthService
{
    Task<LoginResponseDto?> Authenticate(LoginRequestDto loginRequest);
}