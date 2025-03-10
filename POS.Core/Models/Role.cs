
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace POS.Core.Dtos
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        [Required, MaxLength(50)]
        public string RoleName { get; set; } = null!;

        // Consider using a separate RolePermission table instead of a string
        public string? Permissions { get; set; }

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();  // Ensuring proper initialization
    }

}
