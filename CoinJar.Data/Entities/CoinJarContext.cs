using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CoinJar.Data.Entities
{
    public partial class CoinJarContext : DbContext
    {
        public CoinJarContext()
        {
        }

        public CoinJarContext(DbContextOptions<CoinJarContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Coin> Coins { get; set; }
        public virtual DbSet<CoinItem> CoinItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Utilities.DatabaseHelper.ConnectionString);
                optionsBuilder.UseLazyLoadingProxies(true);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Coin>(entity =>
            {
                entity.ToTable("Coin");

                entity.Property(e => e.CoinId)
                    .HasColumnName("CoinID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<CoinItem>(entity =>
            {
                entity.ToTable("CoinItem");

                entity.Property(e => e.CoinItemId)
                    .HasColumnName("CoinItemID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CoinId).HasColumnName("CoinID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DeletionDate).HasColumnType("datetime");

                entity.Property(e => e.Volume).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Coin)
                    .WithMany(p => p.CoinItems)
                    .HasForeignKey(d => d.CoinId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoinItem_Coin");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
