using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebTestTourkit.Models
{
    public partial class ProductManagementContext : DbContext
    {
        public ProductManagementContext()
        {
        }

        public ProductManagementContext(DbContextOptions<ProductManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =(local); database = ProductManagement;uid=sa;pwd=12345678;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasMany(d => d.ProductTypes)
                    .WithMany(p => p.Products)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProductProductType",
                        l => l.HasOne<ProductType>().WithMany().HasForeignKey("ProductTypeId").HasConstraintName("FK__Product_P__Produ__3C69FB99"),
                        r => r.HasOne<Product>().WithMany().HasForeignKey("ProductId").HasConstraintName("FK__Product_P__Produ__3B75D760"),
                        j =>
                        {
                            j.HasKey("ProductId", "ProductTypeId").HasName("PK__Product___4E1FD43BA7DC05F7");

                            j.ToTable("Product_ProductType");
                        });
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
