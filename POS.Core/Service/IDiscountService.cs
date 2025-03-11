using POS.Core.Models.DiscountDTOs;
using POS.Core.Models;

namespace POS.Core.Service
{
    public interface IDiscountService
    {
        Task<decimal> ApplyDiscountAsync(int discountId, decimal amount);
        Task<bool> RemoveDiscountAsync(int discountId);
        Task<DiscountDto> GetDiscountAsync(int discountId);
        Task CreateDiscountAsync(DiscountCreateDto discountCreateDto);
        Task UpdateDiscountAsync(DiscountUpdateDto discountUpdateDto);
    }

}
