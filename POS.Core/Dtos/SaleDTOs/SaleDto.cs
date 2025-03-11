using POS.Core.Models.SaleItemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Models.SaleDTOs
{
    public class SaleDto
    {
        public int SaleId { get; set; }
        public int UserId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int? DiscountId { get; set; }
        public int? TaxId { get; set; }
    }

    public class SaleCreateDto
    {
        public int UserId { get; set; }
        public int? DiscountId { get; set; }
        public int? TaxId { get; set; }
        public List<SaleItemsCreateDto> SaleItems { get; set; } = new();
    }

    public class SaleReceiptDto
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SaleItemDto> SaleItems { get; set; } = new();
    }

    public class SaleItemsDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class SaleItemsCreateDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
