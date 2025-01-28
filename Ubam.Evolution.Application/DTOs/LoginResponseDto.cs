namespace Application.DTOs;

public class LoginResponseDto
{
    public required string Token { get; set; }
    public required string Username { get; set; }
}