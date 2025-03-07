using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Dtos.ReceiptDTOs
{
    public class ReceiptDto
    {
        public int ReceiptId { get; set; }
        public int SaleId { get; set; }
        public DateTime GeneratedDate { get; set; }
        public string? ReceiptContent { get; set; }
    }

    public class ReceiptCreateDto
    {
        public int SaleId { get; set; }
        public string ReceiptContent { get; set; } = string.Empty;
    }
}
