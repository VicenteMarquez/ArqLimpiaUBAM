using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Ubam.Evolution.Application.Contracts;
using Ubam.Evolution.Domain.Entities;
using Ubam.Evolution.Domain.Interfaces;

namespace Ubam.Evolution.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Autentica un usuario verificando su nombre de usuario y contraseña.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <returns>Objeto User si la autenticación es exitosa; null si falla.</returns>
        public async Task<User> Authenticate(string username, string password)
        {
            // Busca el usuario por nombre de usuario de forma asincrónica
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
            {
                // Retorna null si la autenticación falla
                return null;
            }

            // Retorna el usuario si la autenticación es exitosa
            return user;
        }

        /// <summary>
        /// Crea un nuevo usuario con un rol específico y una contraseña encriptada.
        /// </summary>
        /// <param name="user">Objeto de usuario a crear.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <returns>True si el usuario fue creado exitosamente; False si falla.</returns>
        public async Task<bool> CreateUser(User user, string password)
        {
            // Verifica si el nombre de usuario ya existe
            var existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);
            if (existingUser != null)
            {
                return false; // El nombre de usuario ya está en uso
            }

            // Crea un hash de la contraseña y asigna el hash al usuario
            user.PasswordHash = HashPassword(password);

            // Llama al repositorio para guardar el usuario
            await _userRepository.CreateUserAsync(user);
            return true;
        }

        /// <summary>
        /// Verifica el hash de una contraseña.
        /// </summary>
        /// <param name="password">Contraseña a verificar.</param>
        /// <param name="storedHash">Hash almacenado para comparar.</param>
        /// <returns>True si el hash coincide; False si no.</returns>
        private bool VerifyPasswordHash(string password, string storedHash)
        {
            // Verifica la contraseña ingresada contra el hash almacenado
            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return computedHash == storedHash;
        }

        /// <summary>
        /// Crea un hash seguro de la contraseña.
        /// </summary>
        /// <param name="password">Contraseña en texto plano.</param>
        /// <returns>Hash de la contraseña en Base64.</returns>
        private string HashPassword(string password)
        {
            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
