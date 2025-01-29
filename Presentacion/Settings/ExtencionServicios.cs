using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Presentacion.Settings
{
    public static class ExtencionServicios
    {
        public static void ConfiguracionJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtkey = configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtkey))
            {
                throw new ArgumentNullException("La clave JWT no está disponible");
            }

            var key = Encoding.ASCII.GetBytes(jwtkey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = true
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.HttpContext.Request.Cookies["AuthToken"];
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.Redirect("/accesodenegado");
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.Redirect("/accesodenegado");
                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        context.Response.Redirect("/accesodenegado");
                        return Task.CompletedTask;
                    }
                };

            });

        }
    }
}
