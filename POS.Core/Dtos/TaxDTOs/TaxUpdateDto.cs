using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Dtos.TaxDTOs
{
    public class TaxUpdateDto
    {
        public int TaxId { get; set; }
        public decimal TaxPercentage { get; set; }
        public string? Region { get; set; }
    }
}
