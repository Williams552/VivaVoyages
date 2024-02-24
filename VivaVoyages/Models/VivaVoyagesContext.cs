using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VivaVoyages.Models;

public partial class VivaVoyagesContext : DbContext
{
    public VivaVoyagesContext()
    {
    }

    public VivaVoyagesContext(DbContextOptions<VivaVoyagesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Consist> Consists { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    public virtual DbSet<PriceComponent> PriceComponents { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TienDat\\MYSQLSERVER;uid=sa;pwd=tiendat2003;database=VivaVoyages;encrypt=true;trustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA5863D872292");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Consist>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Consist");

            entity.Property(e => e.CouponCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.HasOne(d => d.CouponCodeNavigation).WithMany()
                .HasForeignKey(d => d.CouponCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Consist__CouponC__4BAC3F29");

            entity.HasOne(d => d.Order).WithMany()
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Consist__OrderID__4CA06362");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.CouponCode).HasName("PK__Coupons__D34908018D6E5FFF");

            entity.Property(e => e.CouponCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Discount).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAFEBDC0EA5");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("OrderID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TourId).HasColumnName("TourID");

            entity.HasOne(d => d.Account).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__AccountI__3F466844");

            entity.HasOne(d => d.Tour).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__TourID__403A8C7D");
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.HasOne(d => d.Order).WithMany()
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Passenger__Order__49C3F6B7");
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.HasKey(e => e.PlaceId).HasName("PK__Places__D5222B4E47A67C2E");

            entity.Property(e => e.PlaceId)
                .ValueGeneratedNever()
                .HasColumnName("PlaceID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlaceName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TourId).HasColumnName("TourID");

            entity.HasOne(d => d.Tour).WithMany(p => p.Places)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Places__TourID__440B1D61");
        });

        modelBuilder.Entity<PriceComponent>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PriceComponent");

            entity.Property(e => e.ExpectedProfit).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Tax).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TourId).HasColumnName("TourID");

            entity.HasOne(d => d.Tour).WithMany()
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PriceComp__TourI__45F365D3");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__96D4AAF7F7E1D1DE");

            entity.Property(e => e.StaffId)
                .ValueGeneratedNever()
                .HasColumnName("StaffID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.TourId).HasName("PK__Tour__604CEA1024BB1DC6");

            entity.ToTable("Tour");

            entity.Property(e => e.TourId)
                .ValueGeneratedNever()
                .HasColumnName("TourID");
            entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Destination)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TourDates)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TourGuide)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
