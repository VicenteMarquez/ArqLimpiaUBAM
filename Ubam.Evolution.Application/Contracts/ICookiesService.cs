using Application.DTOs;

namespace Application.Contracts;

public interface ICookiesService
{
    Task SaveUserAsync(UserDto authUser, string token);
    Task ClearUserAsync();
}