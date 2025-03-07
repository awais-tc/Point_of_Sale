using AutoMapper;
using POS.Core.Dtos.ReceiptDTOs;
using POS.Core.Dtos.SaleDTOs;
using POS.Core.Models;
using POS.Core.Repository;
using POS.Core.Service;
using System.Text;

namespace POS.Service
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public ReceiptService(
            IReceiptRepository receiptRepository,
            ISaleRepository saleRepository,
            IMapper mapper)
        {
            _receiptRepository = receiptRepository;
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<ReceiptDto> GenerateReceiptAsync(SaleDto saleDto)
        {
            var sale = await _saleRepository.GetSaleAsync(saleDto.SaleId);
            if (sale == null) throw new KeyNotFoundException($"Sale with ID {saleDto.SaleId} not found.");

            var receipt = new Receipt
            {
                SaleId = sale.SaleId,
                GeneratedDate = DateTime.UtcNow,
                ReceiptContent = GenerateReceiptContent(saleDto) // Generating content from SaleDto
            };

            var createdReceipt = await _receiptRepository.CreateReceiptAsync(receipt);

            return _mapper.Map<ReceiptDto>(createdReceipt);
        }

        public async Task<ReceiptDto?> GetReceiptBySaleIdAsync(int saleId)
        {
            var receipt = await _receiptRepository.GetReceiptBySaleIdAsync(saleId);
            if (receipt == null) return null;

            return _mapper.Map<ReceiptDto>(receipt);
        }

        public async Task<List<ReceiptDto>> GetAllReceiptsAsync()
        {
            var receipts = await _receiptRepository.GetAllReceiptsAsync();
            return _mapper.Map<List<ReceiptDto>>(receipts);
        }

        private string GenerateReceiptContent(SaleDto saleDto)
        {
            var sb = new StringBuilder();
            sb.AppendLine("------ RECEIPT ------");
            sb.AppendLine($"Sale ID: {saleDto.SaleId}");
            sb.AppendLine($"Date: {saleDto.SaleDate}");
            sb.AppendLine($"Total Amount: ${saleDto.TotalAmount}");
            sb.AppendLine("---------------------");
            return sb.ToString();
        }
    }

}
