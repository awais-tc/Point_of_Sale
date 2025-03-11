using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Models.DiscountDTOs
{
    public class DiscountDto
    {
        public int DiscountId { get; set; }
        public string Code { get; set; } = null!;
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MinPurchaseAmount { get; set; }
    }
}
