using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Dtos.PaymentDTOs
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }
        public int SaleId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentType { get; set; } = null!;
        public string? PaymentStatus { get; set; }
    }

    public class PaymentCreateDto
    {
        public int SaleId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentType { get; set; } = null!;
    }

}
