using AutoMapper;
using POS.Core.Dtos.BarcodeDTOs;
using POS.Core.Models;
using POS.Core.Repository;
using POS.Core.Service;

namespace POS.Service
{
    public class BarcodeService : IBarcodeService
    {
        private readonly IBarcodeRepository _barcodeRepository;
        private readonly IMapper _mapper;

        public BarcodeService(IBarcodeRepository barcodeRepository, IMapper mapper)
        {
            _barcodeRepository = barcodeRepository;
            _mapper = mapper;
        }

        public async Task AddBarcodeAsync(BarcodeCreateDto barcodeCreateDto)
        {
            var barcode = _mapper.Map<Barcode>(barcodeCreateDto);
            await _barcodeRepository.AddBarcodeAsync(barcode);
        }

        public async Task<BarcodeDto> GetBarcodeAsync(int barcodeId)
        {
            var barcode = await _barcodeRepository.GetBarcodeAsync(barcodeId);
            if (barcode == null) throw new KeyNotFoundException("Barcode not found.");

            return _mapper.Map<BarcodeDto>(barcode);
        }

        public async Task<IEnumerable<BarcodeDto>> GetBarcodesAsync()
        {
            var barcodes = await _barcodeRepository.GetBarcodesAsync();
            return _mapper.Map<List<BarcodeDto>>(barcodes);
        }

        public async Task UpdateBarcodeAsync(BarcodeUpdateDto barcodeUpdateDto)
        {
            var barcode = await _barcodeRepository.GetBarcodeAsync(barcodeUpdateDto.BarcodeId);
            if (barcode == null) throw new KeyNotFoundException("Barcode not found.");

            _mapper.Map(barcodeUpdateDto, barcode);
            await _barcodeRepository.UpdateBarcodeAsync(barcode);
        }

        public async Task<bool> DeleteBarcodeAsync(int barcodeId)
        {
            return await _barcodeRepository.DeleteBarcodeAsync(barcodeId);
        }

        public async Task<BarcodeDto> GetBarcodeByProductIdAsync(int productId)
        {
            var barcode = await _barcodeRepository.GetBarcodeByProductIdAsync(productId);
            if (barcode == null) throw new KeyNotFoundException("Barcode not found for the given product.");

            return _mapper.Map<BarcodeDto>(barcode);
        }
    }

}
