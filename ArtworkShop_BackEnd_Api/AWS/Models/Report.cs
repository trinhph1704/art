using System;
using System.Collections.Generic;

namespace AWS.Models
{
    public partial class Report
    {
        public string ReportId { get; set; } = null!;
        public string? UserId { get; set; }
        public string? ArtworkId { get; set; }
        public DateTime? ReportDate { get; set; }
        public string? Description { get; set; }

        public virtual Artwork? Artwork { get; set; }
        public virtual Usertb? User { get; set; }
    }
}
