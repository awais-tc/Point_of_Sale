using POS.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Repository
{
    public interface IReceiptRepository
    {
        Task<Receipt> CreateReceiptAsync(Receipt receipt);
        Task<Receipt?> GetReceiptBySaleIdAsync(int saleId);
        Task<List<Receipt>> GetAllReceiptsAsync();
    }

}
