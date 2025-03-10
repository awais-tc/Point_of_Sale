using POS.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Repository
{
    public interface IUserRepository
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task AddUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByUsernameAsync(string username);
        Task UpdateUserAsync(User user);
    }
}
