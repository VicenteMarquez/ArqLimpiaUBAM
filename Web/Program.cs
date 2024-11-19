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

// Configuraci�n de JWT y dependencias del proyecto
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureDependencies();

// Configuraci�n de DbContext con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuraci�n de dependencias manual (si no est�n en ServiceExtensions)
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ILoggerService, LoggerService>();

// Registrar otros servicios
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddSingleton<ILoggerService, LoggerService>();

// A�adir los servicios de controladores
builder.Services.AddControllers();  // <---- Aqu� agregamos los controladores

builder.Services.AddAuthorization();

var app = builder.Build();

// Configuraci�n del pipeline de solicitudes HTTP
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// Usar archivos est�ticos
app.UseStaticFiles();

// Usar autenticaci�n y autorizaci�n
app.UseAuthentication();  // Habilitar autenticaci�n
app.UseAuthorization();  // Habilitar autorizaci�n

// Habilitar la ruta por defecto de MVC
//app.MapDefaultControllerRoute();

// Mapear controladores
app.MapControllers();

app.Run();
