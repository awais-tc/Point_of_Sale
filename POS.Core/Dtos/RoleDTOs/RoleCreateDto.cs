using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Models.RoleDTOs
{
    public class RoleCreateDto
    {
        public string RoleName { get; set; } = null!;
        public string? Permissions { get; set; }
    }
}
