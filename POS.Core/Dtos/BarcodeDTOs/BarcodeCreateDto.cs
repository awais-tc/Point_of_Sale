using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Dtos.BarcodeDTOs
{
    public class BarcodeCreateDto
    {
        public int ProductId { get; set; }
        public string BarcodeNumber { get; set; } = null!;
    }
}
