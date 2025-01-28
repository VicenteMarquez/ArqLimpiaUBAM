namespace Application.DTOs;

public class UserDto
{
    public Guid UserId { get; set; }
    public required string Username { get; set; }
    public required string Role { get; set; }
}