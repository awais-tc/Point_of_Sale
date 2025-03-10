using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Dtos
{
    public class SaleItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleItemId { get; set; }

        [Required]
        public int SaleId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; } // Changed from float to decimal(18,2)

        public int? TaxId { get; set; }

        [ForeignKey(nameof(SaleId))]
        public virtual Sale Sale { get; set; } = null!;

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = null!;

        [ForeignKey(nameof(TaxId))]
        public virtual Tax? Tax { get; set; }
    }

}
