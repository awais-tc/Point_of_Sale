using AutoMapper;
using POS.Core.Dtos.DiscountDTOs;
using POS.Core.Models;
using POS.Core.Repository;
using POS.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Service
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<decimal> ApplyDiscountAsync(int discountId, decimal amount)
        {
            var discount = await _discountRepository.GetDiscountAsync(discountId);
            if (discount == null) throw new KeyNotFoundException("Discount not found.");

            if (amount < discount.MinPurchaseAmount)
                throw new InvalidOperationException("Purchase amount is below the minimum required for this discount.");

            decimal discountAmount = amount * (discount.Percentage / 100);
            return amount - discountAmount;
        }

        public async Task<bool> RemoveDiscountAsync(int discountId)
        {
            return await _discountRepository.DeleteDiscountAsync(discountId);
        }

        public async Task<DiscountDto> GetDiscountAsync(int discountId)
        {
            var discount = await _discountRepository.GetDiscountAsync(discountId);
            if (discount == null) throw new KeyNotFoundException("Discount not found.");

            return _mapper.Map<DiscountDto>(discount);
        }

        public async Task CreateDiscountAsync(DiscountCreateDto discountCreateDto)
        {
            var discount = _mapper.Map<Discount>(discountCreateDto);
            await _discountRepository.AddDiscountAsync(discount);
        }

        public async Task UpdateDiscountAsync(DiscountUpdateDto discountUpdateDto)
        {
            var discount = await _discountRepository.GetDiscountAsync(discountUpdateDto.DiscountId);
            if (discount == null) throw new KeyNotFoundException("Discount not found.");

            _mapper.Map(discountUpdateDto, discount);
            await _discountRepository.UpdateDiscountAsync(discount);
        }
    }
}
