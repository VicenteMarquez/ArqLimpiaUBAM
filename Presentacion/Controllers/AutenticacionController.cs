using Aplicacion.Contratos;
using Aplicacion.Dtos;
using Dominio.Excepciones;
using Infraestructura.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ubam.Evolution.Application.Dtos;
using Ubam.Evolution.Domain.Events;

public class AutenticacionController : Controller
{
  
    private readonly IAccesoServicio _authService;
    private readonly ILoggerServicio _logger;
    private readonly IServicioJWT _jwtService;
    private readonly SetEventsTimes _setEvents;
    private readonly UsuarioMapper _usuarioMapper;
    

    public AutenticacionController(IAccesoServicio authService, ILoggerServicio logger, IServicioJWT jwtService, SetEventsTimes setEvents, UsuarioMapper usuarioMapper)
    {
        _authService = authService;
        _logger = logger;
        _jwtService = jwtService;
        _setEvents = setEvents;
        _usuarioMapper = usuarioMapper;
    }
    public IActionResult Iniciosesion()
    {
        if (User.IsInRole("admin"))
        {
            return RedirectToAction("Index", "Admin");
        }
        if (User.IsInRole("empleado"))
        {
            return RedirectToAction("Index", "Empleado");
        }
        if (User.IsInRole("cliente"))
        {
            return RedirectToAction("Index", "Cliente");
        }
        var ruta = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Autenticacion", "Login.html");
        var contenido = System.IO.File.ReadAllText(ruta);
        return Content(contenido, "text/html");
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] IniciarSesionDto iniciarSesionDto)
    {
        try
        {
            var usuario=_usuarioMapper.ToEntityIniciarSesionDto(iniciarSesionDto);
            _logger.Log($"{_setEvents.EventsTimes(EnumEvento.InicioSesionFallido)} {usuario.Usuario_Nombre}");

            var v1 = await _authService.InciarSesion(usuario.Usuario_Nombre, usuario.Usuario_ContraHash);
            if (v1 == null)
            {
                _logger.Log(_setEvents.EventsTimes(EnumEvento.InicioSesionFallido));
                TempData["Error"] =_setEvents.EventsTimes(EnumEvento.InicioSesionFallido);
                return Ok(new { success = false, redirectUrl = Url.Action("InicioSesion", "Autenticacion") });
            }

            var token = _jwtService.GenerarJWT(v1);
            _logger.Log(_setEvents.EventsTimes(EnumEvento.InicioSesionExitoso)+$"{v1.Usuario_Nombre} su rol es {v1.Usuario_Rol}.");
            _jwtService.SetJwtCookie(token);
            return Ok(new { success = true, redirectUrl = Url.Action("Index", v1.Usuario_Rol.ToLower()) });
        }

        catch (Exception ex)
        {
               TempData["Error"] =_setEvents.EventsTimes(EnumEvento.RegistroFallido);
            _logger.Log(ExceptionModel.EnvioArgument(ex));

            return Ok(new { success = false, redirectUrl = Url.Action("InicioSesion", "Autenticacion") });
        }
    }

    [Authorize(Roles = "admin")]
    [HttpPost("registercompleteuser")]
    public async Task<IActionResult> RegisterCompleteUser([FromBody] UsuarioCompletoDto usuarioCompletoDto)
    {
        try
        {
            string Contrasena = usuarioCompletoDto.Usuario_Contrasena;
            var data = _usuarioMapper.ToEntityUsuarioCompletoDto(usuarioCompletoDto);
            var respuesta = await _authService.CrearUsuarioCompletoAsync(data.Item1, data.Item2, data.Item3, Contrasena);

            if (respuesta)
            {
                _logger.Log($"{_setEvents.EventsTimes(EnumEvento.Registro)} {usuarioCompletoDto.Usuario_Nombre} con rol {usuarioCompletoDto.Usuario_Rol}.");
                TempData["Success"] = $"{_setEvents.EventsTimes(EnumEvento.InicioSesion)} Usuario {usuarioCompletoDto.Usuario_Nombre}";
                return Ok(new { success = true, redirectUrl = Url.Action("Index", "Admin") });
            }

            TempData["Error"] = _setEvents.EventsTimes(EnumEvento.RegistroFallido);
            return Ok(new { success = false, redirectUrl = Url.Action("Registrodato", "Admin") });
        }
        catch (Exception ex)
        {
            ExceptionModel.EnvioArgument(ex);
            TempData["Error"] = ExceptionModel.EnvioArgument(ex);
            return Ok(new { success = false, errorMessage = "Hubo un error al registrar el usuario." });
        }
    }


    [HttpPost("logout")]
    public async Task<IActionResult>Logout()
    {
        _jwtService.DeleteJWT();
        return RedirectToAction("Iniciosesion", "Autenticacion");

    }

    [HttpGet("accesodenegado")]
    public IActionResult Accesodenegado()
    {
        {
            return View();
        }
    }
}
