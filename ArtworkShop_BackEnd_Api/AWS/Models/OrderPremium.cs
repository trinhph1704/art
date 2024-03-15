using System;
using System.Collections.Generic;

namespace AWS.Models
{
    public partial class OrderPremium
    {
        public OrderPremium()
        {
            OrderPremiumLogs = new HashSet<OrderPremiumLog>();
        }

        public string OrderPremiumId { get; set; } = null!;
        public string? UserId { get; set; }
        public string? PremiumId { get; set; }
        public bool? Status { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Total { get; set; }

        public virtual Premium? Premium { get; set; }
        public virtual Usertb? User { get; set; }
        public virtual ICollection<OrderPremiumLog> OrderPremiumLogs { get; set; }
    }
}
