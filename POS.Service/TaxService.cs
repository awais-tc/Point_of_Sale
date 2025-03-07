using AutoMapper;
using POS.Core.Models.TaxDTOs;
using POS.Core.Models;
using POS.Core.Repository;
using POS.Core.Service;
using Microsoft.EntityFrameworkCore;

namespace POS.Service
{
    public class TaxService : ITaxService
    {
        private readonly ITaxRepository _taxRepository;
        private readonly IMapper _mapper;

        public TaxService(ITaxRepository taxRepository, IMapper mapper)
        {
            _taxRepository = taxRepository;
            _mapper = mapper;
        }

        public double CalculateTax(double amount, double taxRate)
        {
            return amount * (taxRate / 100);
        }

        public async Task<TaxDto> GetTaxByRegionAsync(string region)
        {
            var tax = await _taxRepository.GetTaxByRegionAsync(region);
            if (tax == null) throw new KeyNotFoundException("Tax region not found.");

            return _mapper.Map<TaxDto>(tax);
        }

        public async Task<List<TaxDto>> GetAllTaxesAsync()
        {
            var taxes = await _taxRepository.GetAllTaxesAsync();
            return _mapper.Map<List<TaxDto>>(taxes);
        }

        public async Task AddTaxAsync(TaxCreateDto taxCreateDto)
        {
            var tax = _mapper.Map<Tax>(taxCreateDto);
            await _taxRepository.AddTaxAsync(tax);
        }

        public async Task UpdateTaxAsync(TaxUpdateDto taxUpdateDto)
        {
            var tax = await _taxRepository.GetTaxByRegionAsync(taxUpdateDto.Region);
            if (tax == null) throw new KeyNotFoundException("Tax region not found.");

            _mapper.Map(taxUpdateDto, tax);
            await _taxRepository.UpdateTaxAsync(tax);
        }

        public async Task<bool> DeleteTaxAsync(int taxId)
        {
            return await _taxRepository.DeleteTaxAsync(taxId);
        }
    }

}
