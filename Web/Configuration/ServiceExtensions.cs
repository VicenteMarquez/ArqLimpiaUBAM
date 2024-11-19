using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Ubam.Evolution.Application.Contracts;
using Ubam.Evolution.Application.Services;
using Ubam.Evolution.Domain.Interfaces;
using Ubam.Evolution.Infrastructure.Data;

namespace Ubam.Evolution.Presentation.API.Configuration
{
    public static class ServiceExtensions
    {
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            // Verifica si la clave Jwt:Key existe y no es nula o vacía
            var jwtKey = configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentNullException("La clave JWT no está configurada en appsettings.json.");
            }

            var key = Encoding.ASCII.GetBytes(jwtKey);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
