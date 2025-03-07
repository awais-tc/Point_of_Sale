


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Core.Models
{
    public partial class Receipt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReceiptId { get; set; }

        [Required]
        public int SaleId { get; set; }

        [Required]
        public DateTime GeneratedDate { get; set; } = DateTime.UtcNow;  // Default value set

        [MaxLength(5000)]  // Ensures reasonable storage size
        public string? ReceiptContent { get; set; }  // Made nullable if it can be generated later

        [ForeignKey(nameof(SaleId))]
        public virtual Sale Sale { get; set; } = null!;
    }
}
