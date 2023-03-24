using System;
using System.Collections.Generic;
using CakeFactoryProd.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using CakeFactoryProd.ViewModels;

namespace CakeFactoryProd.Data
{
    public partial class CakeFactoryContext : IdentityDbContext
    {
        public CakeFactoryContext(DbContextOptions<CakeFactoryContext> options)
            : base(options)
        {
        }
        public CakeFactoryContext()
        {
        }

        public virtual DbSet<Cake> Cakes { get; set; } = null!;
        public virtual DbSet<CakeHasTopping> CakeHasToppings { get; set; } = null!;
        public virtual DbSet<Filling> Fillings { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderHasCake> OrderHasCakes { get; set; } = null!;
        public virtual DbSet<Shape> Shapes { get; set; } = null!;
        public virtual DbSet<Size> Sizes { get; set; } = null!;
        public virtual DbSet<Topping> Toppings { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cake>(entity =>
            {
                entity.ToTable("Cake");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.FillingId).HasColumnName("fillingId");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("imagePath");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.ShapeId).HasColumnName("shapeId");

                entity.Property(e => e.SizeId).HasColumnName("sizeId");

                entity.HasOne(d => d.Filling)
                    .WithMany(p => p.Cakes)
                    .HasForeignKey(d => d.FillingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cake__fillingId__35BCFE0A");

                entity.HasOne(d => d.Shape)
                    .WithMany(p => p.Cakes)
                    .HasForeignKey(d => d.ShapeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cake__shapeId__37A5467C");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Cakes)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cake__sizeId__36B12243");
            });

            modelBuilder.Entity<CakeHasTopping>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CakeId).HasColumnName("cakeId");

                entity.Property(e => e.ToppingId).HasColumnName("toppingId");

                entity.HasOne(d => d.Cake)
                    .WithMany()
                    .HasForeignKey(d => d.CakeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CakeHasTo__cakeI__398D8EEE");

                entity.HasOne(d => d.Topping)
                    .WithMany()
                    .HasForeignKey(d => d.ToppingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CakeHasTo__toppi__3A81B327");
            });

            modelBuilder.Entity<Filling>(entity =>
            {
                entity.ToTable("Filling");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Flavor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("flavor");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PriceFactor).HasColumnName("priceFactor");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsPicked).HasColumnName("isPicked");

                entity.Property(e => e.OpenOrder).HasColumnName("openOrder");

                entity.Property(e => e.OpenOrderDate)
                    .HasColumnType("date")
                    .HasColumnName("openOrderDate");

                entity.Property(e => e.PickupDate)
                    .HasColumnType("date")
                    .HasColumnName("pickupDate");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnType("date")
                    .HasColumnName("purchaseDate");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("totalAmount");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__userId__3F466844");
            });

            modelBuilder.Entity<OrderHasCake>(entity =>
            {
                entity.HasKey(o => new{o.CakeId, o.OrderId});

                entity.Property(e => e.CakeId).HasColumnName("cakeId");

                entity.Property(e => e.Cost)
                    .HasColumnType("money")
                    .HasColumnName("cost");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Cake)
                    .WithMany()
                    .HasForeignKey(d => d.CakeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderHasC__cakeI__4222D4EF");

                entity.HasOne(d => d.Order)
                    .WithMany()
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderHasC__order__412EB0B6");
            });

            modelBuilder.Entity<Shape>(entity =>
            {
                entity.ToTable("Shape");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PriceFactor).HasColumnName("priceFactor");

                entity.Property(e => e.Value)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CakeBasicPrice)
                    .HasColumnType("money")
                    .HasColumnName("cakeBasicPrice");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Value)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<Topping>(entity =>
            {
                entity.ToTable("Topping");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Flavor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("flavor");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PriceFactor).HasColumnName("priceFactor");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "UQ__User__AB6E6164BF58D47A")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                //entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.Name)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.PreferredName)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("preferredName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<CakeFactoryProd.ViewModels.CakeVM> CakeVM { get; set; } = default!;

        public DbSet<CakeFactoryProd.ViewModels.UserVM> UserVM { get; set; } = default!;

        public DbSet<CakeFactoryProd.ViewModels.UserAdminVM> UserAdminVM { get; set; } = default!;
    }
}