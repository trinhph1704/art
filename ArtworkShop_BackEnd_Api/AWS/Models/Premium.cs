using System;
using System.Collections.Generic;

namespace AWS.Models
{
    public partial class Premium
    {
        public Premium()
        {
            OrderPremia = new HashSet<OrderPremium>();
            Usertbs = new HashSet<Usertb>();
        }

        public string PremiumId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string DayExpire { get; set; } = null!;

        public virtual ICollection<OrderPremium> OrderPremia { get; set; }
        public virtual ICollection<Usertb> Usertbs { get; set; }
    }
}
