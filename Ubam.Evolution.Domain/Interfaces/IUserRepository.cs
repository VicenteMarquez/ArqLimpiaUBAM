using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubam.Evolution.Domain.Entities;

namespace Ubam.Evolution.Domain.Interfaces;
public interface IUserRepository
{
    Task<User> GetUserByUsernameAsync(string username);
    Task CreateUserAsync(User user); // Método para registrar nuevos usuarios
}
