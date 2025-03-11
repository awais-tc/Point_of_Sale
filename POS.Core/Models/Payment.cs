using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        [Required]
        public int SaleId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]  // Better precision for monetary values
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required, MaxLength(50)]
        public string PaymentType { get; set; } = null!; // Consider replacing with an enum

        [MaxLength(20)]
        public string? PaymentStatus { get; set; }  // Made nullable

        [ForeignKey(nameof(SaleId))]
        public virtual Sale Sale { get; set; } = null!;  // Correctly placed foreign key

        public virtual ICollection<UserPayment> UserPayments { get; set; } = new List<UserPayment>();
    }

}
