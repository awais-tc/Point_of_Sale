﻿

namespace POS.Core.Models.InventoryDTOs
{
    public class InventoryCreateDto
    {
        public int ProductId { get; set; }
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; }
    }
}
