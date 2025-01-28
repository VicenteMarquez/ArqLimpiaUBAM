using Application.Contracts;
using Application.Mappers;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Ubam.Evolution.Domain.Interfaces;
using Ubam.Evolution.Presentation.Middlewares;
using Ubam.Evolution.Presentation.Startups;

var builder = WebApplication.CreateBuilder(args);

// using Ubam.Evolution.Presentation.Configuration;
//builder.Services.AddProjectServices(builder.Configuration);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();

// Repositories
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ICookiesService, CookiesesService>();

// Mappers
builder.Services.AddScoped<PersonMapper>();
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<UserRoleMapper>();

builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

builder.Services.AddHttpClient();
builder.Services.AddAuthorization();

var app = builder.Build();

app.ConfigureMiddlewares(app.Environment);
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Use(async (context, next) =>
{
    context.Response.Headers.CacheControl = "no-cache, no-store, must-revalidate";
    context.Response.Headers.Pragma = "no-cache";
    context.Response.Headers.Expires = "0";
    await next();
});

app.Run();