using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Models.TaxDTOs
{
    public class TaxDto
    {
        public int TaxId { get; set; }
        public decimal TaxPercentage { get; set; }
        public string? Region { get; set; }
    }
}
