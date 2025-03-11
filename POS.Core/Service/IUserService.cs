using POS.Core.Models.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Service
{
    public interface IUserService
    {
        Task<UserDto?> Authenticate(string username, string password);
        Task Create(UserCreateDto user);
        Task Delete(int id);
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto?> GetById(int id);
        Task<UserDto?> GetByUsername(string username);
        Task Update(UserUpdateDto user);
    }
}
