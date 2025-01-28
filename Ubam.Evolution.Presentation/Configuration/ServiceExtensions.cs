using Application.Contracts;
using Application.Mappers;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Ubam.Evolution.Domain.Interfaces;

namespace Ubam.Evolution.Presentation.Configuration;

public static class ServiceExtensions
{
    public static void AddProjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddHttpContextAccessor();

        // Repositories
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<ILoginRepository, LoginRepository>();
        services.AddScoped<IRolRepository, RolRepository>();

        // Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ICookiesService, CookiesesService>();

        // Mappers
        services.AddScoped<PersonMapper>();
        services.AddScoped<UserMapper>();
        services.AddScoped<UserRoleMapper>();

        services.AddControllersWithViews();
        services.AddMemoryCache();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
                options.AccessDeniedPath = "/Auth/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
            });

        services.AddHttpClient();
        services.AddAuthorization();
    }
}