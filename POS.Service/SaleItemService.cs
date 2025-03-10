using AutoMapper;
using POS.Core.Dtos.SaleItemDTOs;
using POS.Core.Dtos;
using POS.Core.Repository;
using POS.Core.Service;

namespace POS.Service
{
    public class SaleItemService : ISaleItemService
    {
        private readonly ISaleItemRepository _saleItemRepository;
        private readonly IMapper _mapper;

        public SaleItemService(ISaleItemRepository saleItemRepository, IMapper mapper)
        {
            _saleItemRepository = saleItemRepository;
            _mapper = mapper;
        }

        public async Task AddSaleItemAsync(SaleItemCreateDto saleItemDto)
        {
            var saleItem = _mapper.Map<SaleItem>(saleItemDto);
            await _saleItemRepository.AddSaleItemAsync(saleItem);
        }

        public async Task<SaleItemDto> GetSaleItemAsync(int saleItemId)
        {
            var saleItem = await _saleItemRepository.GetSaleItemAsync(saleItemId);
            if (saleItem == null) throw new KeyNotFoundException("Sale item not found.");

            return _mapper.Map<SaleItemDto>(saleItem);
        }

        public async Task<IEnumerable<SaleItemDto>> GetSaleItemsAsync()
        {
            var saleItems = await _saleItemRepository.GetAllSaleItemsAsync();
            return _mapper.Map<IEnumerable<SaleItemDto>>(saleItems);
        }

        public async Task UpdateSaleItemAsync(SaleItemUpdateDto saleItemDto)
        {
            var saleItem = await _saleItemRepository.GetSaleItemAsync(saleItemDto.SaleItemId);
            if (saleItem == null) throw new KeyNotFoundException("Sale item not found.");

            _mapper.Map(saleItemDto, saleItem);
            await _saleItemRepository.UpdateSaleItemAsync(saleItem);
        }

        public async Task<bool> DeleteSaleItemAsync(int saleItemId)
        {
            return await _saleItemRepository.DeleteSaleItemAsync(saleItemId);
        }
    }

}
