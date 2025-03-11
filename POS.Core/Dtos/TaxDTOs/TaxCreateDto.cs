

namespace POS.Core.Models.TaxDTOs
{
    public class TaxCreateDto
    {
        public decimal TaxPercentage { get; set; }
        public string? Region { get; set; }
    }
}
