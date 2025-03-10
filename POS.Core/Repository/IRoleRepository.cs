using POS.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Repository
{
    public interface IRoleRepository
    {
        Task AddRoleAsync(Role role);
        Task UpdateRoleAsync(Role role);
        Task DeleteRoleAsync(int roleId);
        Task<Role?> GetRoleByIdAsync(int roleId);
        Task<IEnumerable<Role>> GetAllRolesAsync();
    }
}
