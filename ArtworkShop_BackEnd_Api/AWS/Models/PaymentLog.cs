using System;
using System.Collections.Generic;

namespace AWS.Models
{
    public partial class PaymentLog
    {
        public string PaymentLogId { get; set; } = null!;
        public string? PaymentId { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? Status { get; set; }
        public string? TransactionCode { get; set; }

        public virtual Payment? Payment { get; set; }
    }
}
