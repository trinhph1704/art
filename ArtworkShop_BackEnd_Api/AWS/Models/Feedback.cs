using System;
using System.Collections.Generic;

namespace AWS.Models
{
    public partial class Feedback
    {
        public string FeedbackId { get; set; } = null!;
        public string? UserId { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? Rating { get; set; }

        public virtual Usertb? User { get; set; }
    }
}
