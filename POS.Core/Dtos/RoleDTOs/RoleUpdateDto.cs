using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Dtos.RoleDTOs
{
    public class RoleUpdateDto
    {
        public int RoleId { get; set; } // Needed for identifying the role
        public string RoleName { get; set; } = null!;
        public string? Permissions { get; set; }
    }
}
