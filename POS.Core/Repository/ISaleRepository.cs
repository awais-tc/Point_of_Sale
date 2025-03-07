using POS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Repository
{
    public interface ISaleRepository
    {
        Task<Sale> CreateSaleAsync(Sale sale);
        Task<Sale?> GetSaleAsync(int saleId);
        Task<IEnumerable<Sale>> GetAllSalesAsync();
    }

}
