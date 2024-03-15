using System;
using System.Collections.Generic;

namespace AWS.Models
{
    public partial class Payment
    {
        public Payment()
        {
            PaymentLogs = new HashSet<PaymentLog>();
        }

        public string PaymentId { get; set; } = null!;
        public string? OrderId { get; set; }
        public bool? Status { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? TransactionCode { get; set; }
        public DateTime? VnpTransDate { get; set; }
        public bool? StatusCancle { get; set; }

        public virtual Ordertb? Order { get; set; }
        public virtual ICollection<PaymentLog> PaymentLogs { get; set; }
    }
}
