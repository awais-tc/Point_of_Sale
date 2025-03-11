using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS.Core.Models
{
    public class Barcode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BarcodeId { get; set; }  // Removed StringLength(450)

        [Required]
        public int ProductId { get; set; }  // Changed from string to int to match ProductId type

        [Required]
        [StringLength(50)]  // Adjusted max length for barcode numbers
        public string BarcodeNumber { get; set; } = null!;

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = null!;
    }
}
