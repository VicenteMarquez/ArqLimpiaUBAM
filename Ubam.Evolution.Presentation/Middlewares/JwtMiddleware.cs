using Application.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace Ubam.Evolution.Presentation.Middlewares;

public class JwtMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, IJwtService jwtService, ICookiesService cookiesService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (!string.IsNullOrEmpty(token))
            try
            {
                var user = jwtService.ValidateToken(token);
                context.Items["User"] = user;
            }
            catch (SecurityTokenExpiredException)
            {
                await cookiesService.ClearUserAsync();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

        await next(context);
    }
}