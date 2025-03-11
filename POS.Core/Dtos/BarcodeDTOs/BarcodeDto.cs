

namespace POS.Core.Models.BarcodeDTOs
{
    public class BarcodeDto
    {
        public int BarcodeId { get; set; }
        public int ProductId { get; set; }
        public string BarcodeNumber { get; set; } = null!;
    }
}
