using POS.Core.Dtos.PaymentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Service
{
    public interface IPaymentService
    {
        Task<PaymentDto> ProcessPaymentAsync(PaymentCreateDto paymentDto);
        Task UpdatePaymentStatusAsync(int paymentId, string status);
        Task<PaymentDto?> GetPaymentByIdAsync(int paymentId);
        Task<List<PaymentDto>> GetAllPaymentsAsync();
    }

}
