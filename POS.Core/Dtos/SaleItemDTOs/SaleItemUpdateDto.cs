﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Models.SaleItemDTOs
{
    public class SaleItemUpdateDto
    {
        public int SaleItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int? TaxId { get; set; }
    }
}
