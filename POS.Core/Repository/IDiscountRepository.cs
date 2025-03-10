using POS.Core.Dtos;

namespace POS.Core.Repository
{
    public interface IDiscountRepository
    {
        Task<Discount?> GetDiscountAsync(int discountId);
        Task<List<Discount>> GetAllDiscountsAsync();
        Task<Discount?> GetDiscountByCodeAsync(string code);
        Task AddDiscountAsync(Discount discount);
        Task UpdateDiscountAsync(Discount discount);
        Task<bool> DeleteDiscountAsync(int discountId);
    }

}
