using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Dtos.UserDTOs
{
    public class UserCreateDto
    {
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!; // Will be hashed before saving
        public string? ContactInfo { get; set; }
        public int RoleId { get; set; }
    }
}
