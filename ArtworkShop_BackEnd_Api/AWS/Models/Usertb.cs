using System;
using System.Collections.Generic;

namespace AWS.Models
{
    public partial class Usertb
    {
        public Usertb()
        {
            Artworks = new HashSet<Artwork>();
            Comments = new HashSet<Comment>();
            LikeCollections = new HashSet<LikeCollection>();
            OrderPremia = new HashSet<OrderPremium>();
            Ordertbs = new HashSet<Ordertb>();
            Reports = new HashSet<Report>();
            Roles = new HashSet<Role>();
        }

        public string UserId { get; set; } = null!;
        public string? RoleId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Fullname { get; set; }
        public string? Sex { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Noti { get; set; }
        public string? PremiumId { get; set; }
        public decimal? Money { get; set; }
        public bool? StatusPost { get; set; }
        public string? Bank { get; set; }
        public string? BankAccount { get; set; }

        public virtual Premium? Premium { get; set; }
        public virtual ICollection<Artwork> Artworks { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<LikeCollection> LikeCollections { get; set; }
        public virtual ICollection<OrderPremium> OrderPremia { get; set; }
        public virtual ICollection<Ordertb> Ordertbs { get; set; }
        public virtual ICollection<Report> Reports { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
