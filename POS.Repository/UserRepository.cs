using POS.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using POS.Core.Repository;
using POS.Repository.Context;

public class UserRepository : IUserRepository
{
    private readonly POSDbContext _context;

    public UserRepository(POSDbContext context)
    {
        _context = context;
    }

    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == password);
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.Include(u => u.Role).ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserId == id);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
