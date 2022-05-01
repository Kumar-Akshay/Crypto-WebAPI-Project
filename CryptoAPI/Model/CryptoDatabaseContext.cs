using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CryptoAPI.Model
{
    public partial class CryptoDatabaseContext : DbContext
    {
        public CryptoDatabaseContext()
        {
        }

        public CryptoDatabaseContext(DbContextOptions<CryptoDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Currencycategory> Currencycategories { get; set; } = null!;
        public virtual DbSet<Currencysymbol> Currencysymbols { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;port=3306;database=CryptoDatabase;user=root;password=Nature2021;persist security info=False;connect timeout=300", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Currencycategory>(entity =>
            {
                entity.ToTable("currencycategory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .HasColumnName("CATEGORY");
            });

            modelBuilder.Entity<Currencysymbol>(entity =>
            {
                entity.ToTable("currencysymbol");

                entity.HasIndex(e => e.CategoryId, "CategoryId");

                entity.Property(e => e.MarketCap).HasColumnType("mediumtext");

                entity.Property(e => e.Price)
                    .HasPrecision(19, 2)
                    .HasColumnName("PRICE");

                entity.Property(e => e.Symbol)
                    .HasMaxLength(50)
                    .HasColumnName("SYMBOL");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Currencysymbols)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("currencysymbol_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
