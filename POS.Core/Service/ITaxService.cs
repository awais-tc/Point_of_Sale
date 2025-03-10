﻿using POS.Core.Dtos;
using POS.Core.Dtos.TaxDTOs;

namespace POS.Core.Service
{
    public interface ITaxService
    {
        double CalculateTax(double amount, double taxRate);
        Task<TaxDto> GetTaxByRegionAsync(string region);
        Task<List<TaxDto>> GetAllTaxesAsync();
        Task AddTaxAsync(TaxCreateDto taxCreateDto);
        Task UpdateTaxAsync(TaxUpdateDto taxUpdateDto);
        Task<bool> DeleteTaxAsync(int taxId);
    }
}
