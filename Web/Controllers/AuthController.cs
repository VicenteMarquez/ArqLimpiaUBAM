using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ubam.Evolution.Application.Contracts;
using Ubam.Evolution.Application.DTOs;
using Ubam.Evolution.Domain.Entities;

namespace Ubam.Evolution.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILoggerService _logger;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthService authService, ILoggerService logger, IJwtService jwtService)
        {
            _authService = authService;
            _logger = logger;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            // Log intento de inicio de sesión
            _logger.Log($"Intento de inicio de sesión para el usuario: {userLogin.Username}");

            // Autenticar usuario de forma asincrónica
            var user = await _authService.Authenticate(userLogin.Username, userLogin.Password);
            if (user == null)
            {
                _logger.Log("Inicio de sesión fallido.");
                return Unauthorized("Usuario o contraseña incorrectos.");
            }

            // Generar JWT token
            var token = _jwtService.GenerateJwtToken(user);

            // Log éxito de inicio de sesión
            _logger.Log($"Usuario {user.Username} inició sesión exitosamente.");

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegister)
        {
            // Assign a default role if none is provided
            var newUser = new User
            {
                Username = userRegister.Username,
                Role = userRegister.Role ?? "Usuario" // Default to "Usuario" role if none provided
            };

            // Attempt to create the user
            var success = await _authService.CreateUser(newUser, userRegister.Password);
            if (success)
            {
                _logger.Log($"Usuario {newUser.Username} registrado exitosamente con rol {newUser.Role}.");
                return Ok("Usuario registrado exitosamente.");
            }
            return BadRequest("Error al registrar el usuario.");
        }
    }
}
