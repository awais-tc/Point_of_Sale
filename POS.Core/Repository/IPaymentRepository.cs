using POS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Repository
{
    public interface IPaymentRepository
    {
        Task<Payment> ProcessPaymentAsync(Payment payment);
        Task UpdatePaymentStatusAsync(Payment payment);
        Task<Payment?> GetPaymentByIdAsync(int paymentId);
        Task<List<Payment>> GetAllPaymentsAsync();
    }

}
