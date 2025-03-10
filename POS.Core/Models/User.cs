using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Dtos
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(50)]
        public string Username { get; set; } = null!; // Ensure uniqueness in DbContext

        [Required, MaxLength(255)]
        public string PasswordHash { get; set; } = null!; // Renamed to store hashed passwords

        [MaxLength(200)]
        public string? ContactInfo { get; set; } // Made nullable explicitly

        [Required]
        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; } = null!;

        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

        public virtual ICollection<UserPayment> UserPayments { get; set; } = new List<UserPayment>();
    }

}
