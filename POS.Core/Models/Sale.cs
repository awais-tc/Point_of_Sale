using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Models
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime SaleDate { get; set; } = DateTime.UtcNow; // Default to current timestamp

        [Required]
        public decimal TotalAmount { get; set; }  // Changed to decimal(18,2)

        public int? DiscountId { get; set; }

        public int? TaxId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;

        [ForeignKey(nameof(DiscountId))]
        public virtual Discount? Discount { get; set; }

        [ForeignKey(nameof(TaxId))]
        public virtual Tax? Tax { get; set; }

        public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }

}
