using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Dtos.UserDTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string ContactInfo { get; set; } = null!;
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!; // To show role details
    }
}
