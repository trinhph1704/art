using System;
using System.Collections.Generic;

namespace AWS.Models
{
    public partial class TransactionLog
    {
        public string TransactionId { get; set; } = null!;
        public string? OrderId { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? FeedbackId { get; set; }

        public virtual Ordertb? Order { get; set; }
    }
}
