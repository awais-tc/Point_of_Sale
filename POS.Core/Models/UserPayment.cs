using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Dtos
{
    public class UserPayment
    {
        public int UserId { get; set; }
        public int PaymentId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;

        [ForeignKey(nameof(PaymentId))]
        public virtual Payment Payment { get; set; } = null!;
    }
}
