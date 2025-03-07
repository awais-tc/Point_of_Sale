

namespace POS.Core.Dtos.DiscountDTOs
{
    public class DiscountCreateDto
    {
        public string Code { get; set; } = null!;
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MinPurchaseAmount { get; set; }
    }
}
