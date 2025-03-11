using POS.Core.Models.ReceiptDTOs;
using POS.Core.Models.SaleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Service
{
    public interface IReceiptService
    {
        Task<ReceiptDto> GenerateReceiptAsync(SaleDto saleDto);
        Task<ReceiptDto?> GetReceiptBySaleIdAsync(int saleId);
        Task<List<ReceiptDto>> GetAllReceiptsAsync();
    }

}
