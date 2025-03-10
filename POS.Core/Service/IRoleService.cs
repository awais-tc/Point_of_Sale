using POS.Core.Dtos.RoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Service
{
    public interface IRoleService
    {
        Task CreateRole(RoleCreateDto roleDto);
        Task UpdateRole(RoleUpdateDto roleDto);
        Task DeleteRole(int roleId);
        Task<RoleDto?> GetRole(int roleId);
        Task<IEnumerable<RoleDto>> GetRoles();
    }
}
