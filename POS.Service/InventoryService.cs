using AutoMapper;
using POS.Core.Models.InventoryDTOs;
using POS.Core.Models;
using POS.Core.Repository;
using POS.Core.Service;

namespace POS.Service
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public InventoryService(IInventoryRepository inventoryRepository, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }

        public async Task AddInventoryAsync(InventoryCreateDto inventoryCreateDto)
        {
            var inventory = _mapper.Map<Inventory>(inventoryCreateDto);
            await _inventoryRepository.AddInventoryAsync(inventory);
        }

        public async Task UpdateInventoryAsync(InventoryUpdateDto inventoryUpdateDto)
        {
            var inventory = await _inventoryRepository.GetInventoryAsync(inventoryUpdateDto.InventoryId);
            if (inventory == null) throw new KeyNotFoundException("Inventory not found.");

            _mapper.Map(inventoryUpdateDto, inventory);
            await _inventoryRepository.UpdateInventoryAsync(inventory);
        }

        public async Task<InventoryDto> GetInventoryAsync(int inventoryId)
        {
            var inventory = await _inventoryRepository.GetInventoryAsync(inventoryId);
            if (inventory == null) throw new KeyNotFoundException("Inventory not found.");

            return _mapper.Map<InventoryDto>(inventory);
        }

        public async Task<List<InventoryDto>> GetInventoriesAsync()
        {
            var inventories = await _inventoryRepository.GetInventoriesAsync();
            return _mapper.Map<List<InventoryDto>>(inventories);
        }

        public async Task<List<InventoryDto>> GetInventoriesByProductIdAsync(int productId)
        {
            var inventories = await _inventoryRepository.GetInventoriesByProductIdAsync(productId);
            return _mapper.Map<List<InventoryDto>>(inventories);
        }

        public async Task<bool> DeleteInventoryAsync(int inventoryId)
        {
            return await _inventoryRepository.DeleteInventoryAsync(inventoryId);
        }
    }

}
