using POS.Core.Dtos.SaleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Service
{
    public interface ISaleService
    {
        Task<SaleReceiptDto> CreateSaleAsync(SaleCreateDto saleDto);
        Task<SaleDto> GetSaleAsync(int saleId);
        Task<IEnumerable<SaleDto>> GetAllSalesAsync();
    }
}
