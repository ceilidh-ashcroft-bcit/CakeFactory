//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;

//namespace CakeFactoryProd.Models;

//public partial class CakeFactoryContext : DbContext
//{
//    public CakeFactoryContext()
//    {
//    }

//    public CakeFactoryContext(DbContextOptions<CakeFactoryContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<Ipn> Ipns { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server= LAPTOP-GI8D20M1\\SSD_SQL_SERVER;Database=CakeFactory;Trusted_Connection=True;TrustServerCertificate=True;");

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Ipn>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__IPN__3213E83F616ED7C5");

//            entity.ToTable("IPN");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.Amount)
//                .HasColumnType("money")
//                .HasColumnName("amount");
//            entity.Property(e => e.Cart)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("cart");
//            entity.Property(e => e.CreateTime)
//                .HasColumnType("date")
//                .HasColumnName("create_time");
//            entity.Property(e => e.Currency)
//                .HasMaxLength(3)
//                .IsUnicode(false)
//                .HasColumnName("currency");
//            entity.Property(e => e.Custom)
//                .HasMaxLength(50)
//                .IsUnicode(false)
//                .HasColumnName("custom");
//            entity.Property(e => e.Intent)
//                .HasMaxLength(15)
//                .IsUnicode(false)
//                .HasColumnName("intent");
//            entity.Property(e => e.OrderId).HasColumnName("orderId");
//            entity.Property(e => e.PayerCountryCode)
//                .HasMaxLength(3)
//                .IsUnicode(false)
//                .HasColumnName("payerCountryCode");
//            entity.Property(e => e.PayerEmail)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("payerEmail");
//            entity.Property(e => e.PayerFirstName)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("payerFirstName");
//            entity.Property(e => e.PayerId)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("payerID");
//            entity.Property(e => e.PayerLastName)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("payerLastName");
//            entity.Property(e => e.PayerMiddleName)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("payerMiddleName");
//            entity.Property(e => e.PayerStatus)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("payerStatus");
//            entity.Property(e => e.PaymentId)
//                .HasMaxLength(30)
//                .IsUnicode(false)
//                .HasColumnName("paymentID");
//            entity.Property(e => e.PaymentMethod)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("paymentMethod");
//            entity.Property(e => e.PaymentState)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("paymentState");
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
