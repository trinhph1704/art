using System;
using System.Collections.Generic;

namespace AWS.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Artworks = new HashSet<Artwork>();
        }

        public string GenreId { get; set; } = null!;
        public string? Name { get; set; }

        public virtual ICollection<Artwork> Artworks { get; set; }
    }
}
