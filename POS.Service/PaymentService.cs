using AutoMapper;
using POS.Core.Dtos.PaymentDTOs;
using POS.Core.Models;
using POS.Core.Repository;
using POS.Core.Service;

namespace POS.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public PaymentService(
            IPaymentRepository paymentRepository,
            ISaleRepository saleRepository,
            IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<PaymentDto> ProcessPaymentAsync(PaymentCreateDto paymentDto)
        {
            var sale = await _saleRepository.GetSaleAsync(paymentDto.SaleId);
            if (sale == null) throw new KeyNotFoundException($"Sale with ID {paymentDto.SaleId} not found.");

            var payment = new Payment
            {
                SaleId = paymentDto.SaleId,
                Amount = paymentDto.Amount,
                PaymentDate = DateTime.UtcNow,
                PaymentType = paymentDto.PaymentType,
                PaymentStatus = "Pending" // Default status
            };

            var processedPayment = await _paymentRepository.ProcessPaymentAsync(payment);
            return _mapper.Map<PaymentDto>(processedPayment);
        }

        public async Task UpdatePaymentStatusAsync(int paymentId, string status)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId);
            if (payment == null) throw new KeyNotFoundException($"Payment with ID {paymentId} not found.");

            payment.PaymentStatus = status;
            await _paymentRepository.UpdatePaymentStatusAsync(payment);
        }

        public async Task<PaymentDto?> GetPaymentByIdAsync(int paymentId)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId);
            if (payment == null) return null;

            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<List<PaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _paymentRepository.GetAllPaymentsAsync();
            return _mapper.Map<List<PaymentDto>>(payments);
        }
    }

}
