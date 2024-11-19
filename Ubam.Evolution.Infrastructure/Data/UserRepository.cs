using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ubam.Evolution.Domain.Entities;
using Ubam.Evolution.Domain.Interfaces;

namespace Ubam.Evolution.Infrastructure.Data;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

        if (user == null)
            throw new KeyNotFoundException("User not found.");

        return user;
    }

    public async Task CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
