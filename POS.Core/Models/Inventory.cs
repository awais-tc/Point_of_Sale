
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace POS.Core.Models
{
    public class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]  // Prevents negative stock values
        public int StockQuantity { get; set; }

        [Required]
        [Range(0, int.MaxValue)]  // Ensures reorder level is valid
        public int ReorderLevel { get; set; }

        public DateTime? LastRestocked { get; set; }  // Made optional

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = null!;
    }

}
