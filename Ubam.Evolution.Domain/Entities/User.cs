using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ubam.Evolution.Domain.Entities;
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; } // Contraseña encriptada
    public string Role { get; set; } // Rol del usuario (e.g., "Admin", "User")
}
