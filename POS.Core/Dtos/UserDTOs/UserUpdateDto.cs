using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Models.UserDTOs
{
    public class UserUpdateDto
    {
        public int UserId { get; set; } // Needed to identify which user to update
        public string Name { get; set; } = null!;
        public string ContactInfo { get; set; } = null!;
        public int RoleId { get; set; }
    }
}
