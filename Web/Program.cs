using Microsoft.EntityFrameworkCore;
using Ubam.Evolution.Presentation.API.Configuration;
using Ubam.Evolution.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ubam.Evolution.Application.Contracts;
using Ubam.Evolution.Application.Services;
using Ubam.Evolution.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Agrega soporte para controladores y vistas
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de JWT y dependencias del proyecto
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureDependencies();

// Configuración de DbContext con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de dependencias manual (si no están en ServiceExtensions)
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ILoggerService, LoggerService>();

// Registrar otros servicios
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddSingleton<ILoggerService, LoggerService>();

// Añadir los servicios de controladores
builder.Services.AddControllers();  // <---- Aquí agregamos los controladores

builder.Services.AddAuthorization();

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// Usar archivos estáticos
app.UseStaticFiles();

// Usar autenticación y autorización
app.UseAuthentication();  // Habilitar autenticación
app.UseAuthorization();  // Habilitar autorización

// Habilitar la ruta por defecto de MVC
//app.MapDefaultControllerRoute();

// Mapear controladores
app.MapControllers();

app.Run();
