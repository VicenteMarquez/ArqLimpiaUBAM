using Aplicacion.Contratos;
using Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class ServicioJWT : IServicioJWT
{
    private readonly IConfiguration _config;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ServicioJWT(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _config = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public string GenerarJWT(Usuario usuario)
    {
        var key = _config["Jwt:Key"];
        if (string.IsNullOrEmpty(key))
            throw new InvalidOperationException("JWT key is not configured.");

        var keyBytes = Encoding.ASCII.GetBytes(key);

        var claims = new[]
        {
            new Claim("id", usuario.Usuario_Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Usuario_Nombre),
            new Claim(ClaimTypes.Role, usuario.Usuario_Rol)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
    
    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:Key"])),
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            return principal;
        }
        catch
        {
            return null;
        }
    }
    
    public void SetJwtCookie(string token)
    {
        var response = _httpContextAccessor.HttpContext.Response;
        response.Cookies.Append("AuthToken", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(30)
        });
    }
    public  void DeleteJWT()
    {
        var response = _httpContextAccessor.HttpContext.Response;
        response.Cookies.Delete("AuthToken");
    }
}
