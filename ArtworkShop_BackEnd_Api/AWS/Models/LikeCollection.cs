using System;
using System.Collections.Generic;

namespace AWS.Models
{
    public partial class LikeCollection
    {
        public string UserId { get; set; } = null!;
        public string ArtworkId { get; set; } = null!;
        public DateTime? Time { get; set; }

        public virtual Artwork Artwork { get; set; } = null!;
        public virtual Usertb User { get; set; } = null!;
    }
}
