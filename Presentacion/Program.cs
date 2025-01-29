using Aplicacion.Contratos;
using Aplicacion.Servicios;
using Dominio.Excepciones;
using Dominio.Interfaces;
using Infraestructura.Data;
using Infraestructura.Mapper;
using Infraestructura.Repositories;
using Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Presentacion.Settings;
using Ubam.Evolution.Domain.Events;
using Ubam.Evolution.Domain.Interfaces;
using Ubam.Evolution.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.ConfiguracionJWT(builder.Configuration);
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionBD")));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IContactoRepository, ContactoRepository>();
builder.Services.AddScoped<IServicioJWT, ServicioJWT>();
builder.Services.AddScoped<ILoggerServicio, LoggerServicio>();
builder.Services.AddScoped<IPersonaServicio, PersonaServicio>();
builder.Services.AddScoped<IAccesoServicio, ServicioValidacion>();
builder.Services.AddScoped<IProcedimientosRepository, ProcedimientosRepository>();
builder.Services.AddScoped<SetEventsTimes>();
builder.Services.AddScoped<ExceptionModel>();
builder.Services.AddScoped<UsuarioMapper>();
builder.Services.AddScoped<PersonaMapper>();
builder.Services.AddScoped<ContactoMapper>();
builder.Services.AddAuthorization();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Autenticacion/iniciosesion";
    options.AccessDeniedPath = "/Autenticacion/AccesoDenegado";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("admin", options =>
    options.RequireRole("admin"));
    config.AddPolicy("empleado", options =>
    options.RequireRole("empleado"));
    config.AddPolicy("cliente", options =>
    options.RequireRole("cliente"));
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();



app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Autenticacion}/{action=iniciosesion}/{id?}");

app.Use(async (context, next) =>
{
    context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
    context.Response.Headers["Pragma"] = "no-cache";
    context.Response.Headers["Expires"] = "0";
    await next();
});
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.Run();
