using System;
using System.Collections.Generic;

namespace AWS.Models
{
    public partial class Artwork
    {
        public Artwork()
        {
            Comments = new HashSet<Comment>();
            LikeCollections = new HashSet<LikeCollection>();
            Ordertbs = new HashSet<Ordertb>();
            Reports = new HashSet<Report>();
        }

        public string ArtworkId { get; set; } = null!;
        public string? UserId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? GenreId { get; set; }
        public string? LinkShare { get; set; }
        public int? LikeTimes { get; set; }
        public DateTime? Time { get; set; }
        public string? Reason { get; set; }
        public bool? StatusProcessing { get; set; }
        public DateTime? TimeProcessing { get; set; }
        public string? ImageUrl2 { get; set; }

        public virtual Genre? Genre { get; set; }
        public virtual Usertb? User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<LikeCollection> LikeCollections { get; set; }
        public virtual ICollection<Ordertb> Ordertbs { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
