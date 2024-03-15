using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AWS.Models
{
    public partial class ARTWORKPLATFORMContext : DbContext
    {
        public ARTWORKPLATFORMContext()
        {
        }

        public ARTWORKPLATFORMContext(DbContextOptions<ARTWORKPLATFORMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artwork> Artworks { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<LikeCollection> LikeCollections { get; set; } = null!;
        public virtual DbSet<OrderPremium> OrderPremia { get; set; } = null!;
        public virtual DbSet<OrderPremiumLog> OrderPremiumLogs { get; set; } = null!;
        public virtual DbSet<Ordertb> Ordertbs { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<PaymentLog> PaymentLogs { get; set; } = null!;
        public virtual DbSet<Premium> Premia { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<TransactionLog> TransactionLogs { get; set; } = null!;
        public virtual DbSet<Usertb> Usertbs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-LDHP36H\\SQLEXPRESS;Database=ARTWORKPLATFORM;User Id=sa;Password=12345;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artwork>(entity =>
            {
                entity.ToTable("Artwork");

                entity.Property(e => e.ArtworkId)
                    .HasMaxLength(50)
                    .HasColumnName("ArtworkID");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.GenreId)
                    .HasMaxLength(50)
                    .HasColumnName("GenreID");

                entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");

                entity.Property(e => e.ImageUrl2).HasColumnName("ImageURL2");

                entity.Property(e => e.LikeTimes).HasColumnName("Like_times");

                entity.Property(e => e.LinkShare).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Reason).HasMaxLength(255);

                entity.Property(e => e.StatusProcessing).HasColumnName("Status_Processing");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.TimeProcessing)
                    .HasColumnType("datetime")
                    .HasColumnName("Time_Processing");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Artworks)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK__Artwork__GenreID__6477ECF3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Artworks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Artwork__UserID__656C112C");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentId)
                    .HasMaxLength(50)
                    .HasColumnName("CommentID");

                entity.Property(e => e.ArtworkId)
                    .HasMaxLength(50)
                    .HasColumnName("ArtworkID");

                entity.Property(e => e.Text).HasMaxLength(255);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Artwork)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ArtworkId)
                    .HasConstraintName("FK__Comment__Artwork__66603565");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Comment__UserID__6754599E");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");

                entity.Property(e => e.GenreId)
                    .HasMaxLength(50)
                    .HasColumnName("GenreID");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<LikeCollection>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ArtworkId })
                    .HasName("PK__Like_Col__BA8FF647884725C5");

                entity.ToTable("Like_Collection");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("UserID");

                entity.Property(e => e.ArtworkId)
                    .HasMaxLength(50)
                    .HasColumnName("ArtworkID");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Artwork)
                    .WithMany(p => p.LikeCollections)
                    .HasForeignKey(d => d.ArtworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Like_Coll__Artwo__68487DD7");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LikeCollections)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Like_Coll__UserI__693CA210");
            });

            modelBuilder.Entity<OrderPremium>(entity =>
            {
                entity.ToTable("Order_Premium");

                entity.Property(e => e.OrderPremiumId)
                    .HasMaxLength(255)
                    .HasColumnName("Order_PremiumID");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.PremiumId)
                    .HasMaxLength(50)
                    .HasColumnName("PremiumID");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Premium)
                    .WithMany(p => p.OrderPremia)
                    .HasForeignKey(d => d.PremiumId)
                    .HasConstraintName("FK__Order_Pre__Premi__6A30C649");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderPremia)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Order_Premium_UserID");
            });

            modelBuilder.Entity<OrderPremiumLog>(entity =>
            {
                entity.ToTable("order_premium_log");

                entity.Property(e => e.OrderPremiumLogId)
                    .HasMaxLength(255)
                    .HasColumnName("Order_Premium_LogID");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.OrderPremiumId)
                    .HasMaxLength(255)
                    .HasColumnName("Order_PremiumID");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.OrderPremium)
                    .WithMany(p => p.OrderPremiumLogs)
                    .HasForeignKey(d => d.OrderPremiumId)
                    .HasConstraintName("FK__order_pre__Order__6C190EBB");
            });

            modelBuilder.Entity<Ordertb>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Ordertb__C3905BAF2BF1EB7E");

                entity.ToTable("Ordertb");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(50)
                    .HasColumnName("OrderID");

                entity.Property(e => e.ArtworkId)
                    .HasMaxLength(50)
                    .HasColumnName("ArtworkID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Artwork)
                    .WithMany(p => p.Ordertbs)
                    .HasForeignKey(d => d.ArtworkId)
                    .HasConstraintName("FK__Ordertb__Artwork__6D0D32F4");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ordertbs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Ordertb__UserID__6E01572D");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentId)
                    .HasMaxLength(50)
                    .HasColumnName("PaymentID");

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(50)
                    .HasColumnName("OrderID");

                entity.Property(e => e.TransactionCode).HasMaxLength(50);

                entity.Property(e => e.VnpTransDate).HasColumnType("datetime");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__Payment__OrderID__6EF57B66");
            });

            modelBuilder.Entity<PaymentLog>(entity =>
            {
                entity.ToTable("Payment_Log");

                entity.Property(e => e.PaymentLogId)
                    .HasMaxLength(50)
                    .HasColumnName("PaymentLogID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentId)
                    .HasMaxLength(50)
                    .HasColumnName("PaymentID");

                entity.Property(e => e.TransactionCode).HasMaxLength(255);

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.PaymentLogs)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK__Payment_L__Payme__6FE99F9F");
            });

            modelBuilder.Entity<Premium>(entity =>
            {
                entity.ToTable("Premium");

                entity.Property(e => e.PremiumId)
                    .HasMaxLength(50)
                    .HasColumnName("PremiumID");

                entity.Property(e => e.DayExpire)
                    .HasMaxLength(20)
                    .HasColumnName("Day_expire");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.ReportId)
                    .HasMaxLength(50)
                    .HasColumnName("ReportID");

                entity.Property(e => e.ArtworkId)
                    .HasMaxLength(50)
                    .HasColumnName("ArtworkID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ReportDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Artwork)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.ArtworkId)
                    .HasConstraintName("FK__Report__ArtworkI__70DDC3D8");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Report__UserID__71D1E811");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(50)
                    .HasColumnName("Role_Id");

                entity.Property(e => e.RoleName).HasMaxLength(20);
            });

            modelBuilder.Entity<TransactionLog>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__Transact__55433A4B8F751F86");

                entity.ToTable("Transaction_Log");

                entity.Property(e => e.TransactionId)
                    .HasMaxLength(50)
                    .HasColumnName("TransactionID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FeedbackId)
                    .HasMaxLength(50)
                    .HasColumnName("FeedbackID");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(50)
                    .HasColumnName("OrderID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TransactionLogs)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__Transacti__Order__72C60C4A");
            });

            modelBuilder.Entity<Usertb>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Usertb__1788CCAC0CB42369");

                entity.ToTable("Usertb");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("UserID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Bank).HasMaxLength(10);

                entity.Property(e => e.BankAccount).HasMaxLength(50);

                entity.Property(e => e.DateOfBirth).HasMaxLength(255);

                entity.Property(e => e.Fullname).HasMaxLength(255);

                entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");

                entity.Property(e => e.Money).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Noti).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.PremiumId)
                    .HasMaxLength(50)
                    .HasColumnName("PremiumID");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(50)
                    .HasColumnName("Role_Id");

                entity.Property(e => e.Sex).HasMaxLength(10);

                entity.Property(e => e.StatusPost).HasColumnName("Status_Post");

                entity.Property(e => e.Username).HasMaxLength(255);

                entity.HasOne(d => d.Premium)
                    .WithMany(p => p.Usertbs)
                    .HasForeignKey(d => d.PremiumId)
                    .HasConstraintName("FK__Usertb__PremiumI__75A278F5");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__User_Role__Role___73BA3083"),
                        r => r.HasOne<Usertb>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__User_Role__UserI__74AE54BC"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId").HasName("PK__User_Rol__BA0867E759B07C2B");

                            j.ToTable("User_Role");

                            j.IndexerProperty<string>("UserId").HasMaxLength(50).HasColumnName("UserID");

                            j.IndexerProperty<string>("RoleId").HasMaxLength(50).HasColumnName("Role_Id");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
