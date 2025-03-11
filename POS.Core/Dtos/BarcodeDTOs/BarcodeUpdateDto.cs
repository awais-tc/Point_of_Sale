using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Models.BarcodeDTOs
{
    public class BarcodeUpdateDto
    {
        public int BarcodeId { get; set; }
        public string BarcodeNumber { get; set; } = null!;
    }
}
