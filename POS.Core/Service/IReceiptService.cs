using POS.Core.Dtos.ReceiptDTOs;
using POS.Core.Dtos.SaleDTOs;
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
