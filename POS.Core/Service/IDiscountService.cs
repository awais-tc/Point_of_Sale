using POS.Core.Dtos.DiscountDTOs;
using POS.Core.Dtos;

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
