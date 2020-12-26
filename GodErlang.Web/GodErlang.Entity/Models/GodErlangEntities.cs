using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GodErlang.Entity.Models
{
    public partial class GodErlangEntities : DbContext
    {
        public GodErlangEntities()
        {
        }

        public GodErlangEntities(DbContextOptions<GodErlangEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<GeneralProduct> GeneralProduct { get; set; }
        public virtual DbSet<ProductDetails> ProductDetails { get; set; }
        public virtual DbSet<ProductPriceHistory> ProductPriceHistory { get; set; }
        public virtual DbSet<ProductStatus> ProductStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeneralProduct>(entity =>
            {
                entity.Property(e => e.ActualPrice).HasColumnType("money");

                entity.Property(e => e.ActualPriceDesc).HasMaxLength(100);

                entity.Property(e => e.OfferType).HasMaxLength(500);

                entity.Property(e => e.OriginId).HasMaxLength(100);

                entity.Property(e => e.PriceCurrency).HasMaxLength(100);

                entity.Property(e => e.ProductImages).HasMaxLength(1000);

                entity.Property(e => e.PromotionPrice).HasColumnType("money");

                entity.Property(e => e.PromotionPriceDesc).HasMaxLength(100);

                entity.Property(e => e.RecordTime).HasColumnType("datetime");

                entity.Property(e => e.ReferUrl).HasMaxLength(500);

                entity.Property(e => e.SourceTypeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductDetails>(entity =>
            {
                entity.Property(e => e.ActualPrice).HasColumnType("money");

                entity.Property(e => e.ActualPriceDesc).HasMaxLength(100);

                entity.Property(e => e.OfferType).HasMaxLength(500);

                entity.Property(e => e.OriginId).HasMaxLength(100);

                entity.Property(e => e.PriceCurrency).HasMaxLength(100);

                entity.Property(e => e.ProductImages).HasMaxLength(1000);

                entity.Property(e => e.PromotionPrice).HasColumnType("money");

                entity.Property(e => e.PromotionPriceDesc).HasMaxLength(100);

                entity.Property(e => e.RecordTime).HasColumnType("datetime");

                entity.Property(e => e.ReferUrl).HasMaxLength(500);

                entity.Property(e => e.SourceTypeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductPriceHistory>(entity =>
            {
                entity.Property(e => e.ActualPrice).HasColumnType("money");

                entity.Property(e => e.PriceCurrency).HasMaxLength(100);

                entity.Property(e => e.PromotionPrice).HasColumnType("money");

                entity.Property(e => e.RecordTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductStatus>(entity =>
            {
                entity.Property(e => e.AddTime).HasColumnType("datetime");

                entity.Property(e => e.EndExecTime).HasColumnType("datetime");

                entity.Property(e => e.ExecMessage).HasMaxLength(200);

                entity.Property(e => e.ReferUrl).HasMaxLength(500);

                entity.Property(e => e.StartExecTime).HasColumnType("datetime");
            });
        }
    }
}
