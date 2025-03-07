using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Models.InventoryDTOs
{
    public class InventoryUpdateDto
    {
        public int InventoryId { get; set; }
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; }
        public DateTime? LastRestocked { get; set; }
    }
}
