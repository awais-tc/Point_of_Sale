using AutoMapper;
using POS.Core.Models.RoleDTOs;
using POS.Core.Models;
using POS.Core.Repository;
using POS.Core.Service;

namespace POS.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task CreateRole(RoleCreateDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            await _roleRepository.AddRoleAsync(role);
        }

        public async Task UpdateRole(RoleUpdateDto roleDto)
        {
            var existingRole = await _roleRepository.GetRoleByIdAsync(roleDto.RoleId);
            if (existingRole != null)
            {
                _mapper.Map(roleDto, existingRole);
                await _roleRepository.UpdateRoleAsync(existingRole);
            }
        }

        public async Task DeleteRole(int roleId)
        {
            await _roleRepository.DeleteRoleAsync(roleId);
        }

        public async Task<RoleDto?> GetRole(int roleId)
        {
            var role = await _roleRepository.GetRoleByIdAsync(roleId);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<IEnumerable<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllRolesAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }
    }

}
