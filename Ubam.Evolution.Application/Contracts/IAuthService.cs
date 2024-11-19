using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubam.Evolution.Domain.Entities;

namespace Ubam.Evolution.Application.Contracts
{
    public interface IAuthService
    {
        Task<User> Authenticate(string username, string password);
        Task<bool> CreateUser(User user, string password);
    }
}

