
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.Core.Models
{
    public class Tax
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TaxId { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Tax percentage must be between 0 and 100.")]
        public decimal TaxPercentage { get; set; } // Changed from float to decimal(5,2)

        public string? Region { get; set; }

        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

        public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }
}
