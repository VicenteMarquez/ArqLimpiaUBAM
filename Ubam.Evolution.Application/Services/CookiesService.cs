using System.Security.Claims;
using Application.Contracts;
using Application.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class CookiesesService(IHttpContextAccessor httpContextAccessor) : ICookiesService
{
    public async Task SaveUserAsync(UserDto authUser, string token)
    {
        var claims = GenerateClaims(authUser, token);

        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext == null) throw new InvalidOperationException("HttpContext no está disponible.");

        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)),
            new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
            }
        );
    }

    public async Task ClearUserAsync()
    {
        var httpContext = httpContextAccessor.HttpContext;
        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    private IEnumerable<Claim> GenerateClaims(UserDto authUser, string token)
    {
        return new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, authUser.UserId.ToString()),
            new(ClaimTypes.Name, authUser.Username),
            new(ClaimTypes.Role, authUser.Role),
            new("jwt", token)
        };
    }
}