using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Dtos
{
    public class Discount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiscountId { get; set; }

        [Required]
        [MaxLength(50)]  // Ensures discount code length is reasonable
        public string Code { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(5,2)")] // Allows values like 99.99% max
        public decimal Percentage { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Better precision for monetary values
        public decimal MinPurchaseAmount { get; set; }

        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }

}
