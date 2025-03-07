using POS.Core.Models;

namespace POS.Core.Repository
{
    public interface ITaxRepository
    {
        Task<Tax?> GetTaxByRegionAsync(string region);
        Task<List<Tax>> GetAllTaxesAsync();
        Task AddTaxAsync(Tax tax);
        Task UpdateTaxAsync(Tax tax);
        Task<bool> DeleteTaxAsync(int taxId);
        Task<Tax?> GetTaxAsync(int taxId);
    }
}
