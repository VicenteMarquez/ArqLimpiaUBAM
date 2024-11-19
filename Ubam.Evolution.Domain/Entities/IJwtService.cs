using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubam.Evolution.Domain.Entities;

namespace Ubam.Evolution.Application.Contracts
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
    }
}
