using System;
using System.Collections.Generic;

namespace AWS.Models
{
    public partial class Comment
    {
        public string CommentId { get; set; } = null!;
        public string? UserId { get; set; }
        public string? ArtworkId { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? Text { get; set; }

        public virtual Artwork? Artwork { get; set; }
        public virtual Usertb? User { get; set; }
    }
}
