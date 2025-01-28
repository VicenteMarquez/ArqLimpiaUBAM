using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Contracts;
using Application.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Ubam.Evolution.Domain.Enum;
using Ubam.Evolution.Domain.Exceptions;

namespace Application.Services;

public class JwtService : IJwtService
{
    private readonly string _jwtAudience;
    private readonly string _jwtIssuer;
    private readonly string _jwtKey;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public JwtService(IConfiguration configuration)
    {
        _jwtKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException();
        _jwtIssuer = configuration["Jwt:Issuer"] ?? throw new InvalidOperationException();
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public string GenerateJwtToken(UserDto user)
    {
        var key = Encoding.UTF8.GetBytes(_jwtKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(60),
            //Issuer = _jwtIssuer,
            //Audience = _jwtAudience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = _tokenHandler.CreateToken(tokenDescriptor);
        return _tokenHandler.WriteToken(token);
    }

    public UserDto ValidateToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) throw new ValidationException(ExceptionEnum.TokenMissing);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtKey);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);

        var authUser = new UserDto
        {
            UserId = Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                                throw new InvalidOperationException()),
            Username = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value ?? throw new InvalidOperationException(),
            Role = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value ?? throw new InvalidOperationException()
        };

        return authUser;
    }
}