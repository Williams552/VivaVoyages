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

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Destination> Destinations { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-KKL2EJV\\SQLEXPRESS;Database=VivaVoyages;uid=sa;pwd=123123;encrypt=true;trustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.CouponCode).HasName("PK__Coupons__D34908015FA41212");

            entity.Property(e => e.CouponCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Discount).HasColumnType("money");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8310C889D");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ResetCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Destination>(entity =>
        {
            entity.HasKey(e => e.DestinationId).HasName("PK__Destinat__DB5FE4AC6BFA7178");

            entity.ToTable("Destination");

            entity.Property(e => e.DestinationId).HasColumnName("DestinationID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlaceId).HasColumnName("PlaceID");
            entity.Property(e => e.TourId).HasColumnName("TourID");

            entity.HasOne(d => d.Place).WithMany(p => p.Destinations)
                .HasForeignKey(d => d.PlaceId)
                .HasConstraintName("FK__Destinati__Place__398D8EEE");

            entity.HasOne(d => d.Tour).WithMany(p => p.Destinations)
                .HasForeignKey(d => d.TourId)
                .HasConstraintName("FK__Destinati__TourI__38996AB5");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAFF992B534");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CouponCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("money");
            entity.Property(e => e.TourId).HasColumnName("TourID");

            entity.HasOne(d => d.CouponCodeNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CouponCode)
                .HasConstraintName("FK__Orders__CouponCo__300424B4");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Customer__2D27B809");

            entity.HasOne(d => d.Staff).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Orders__StaffID__2E1BDC42");

            entity.HasOne(d => d.Tour).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__TourID__2F10007B");
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.PassengerId).HasName("PK__Passenge__88915F908ADF224B");

            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Passengers)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Passenger__Custo__35BCFE0A");

            entity.HasOne(d => d.Order).WithMany(p => p.Passengers)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Passenger__Order__34C8D9D1");
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.HasKey(e => e.PlaceId).HasName("PK__Places__D5222B4EB2A9451F");

            entity.Property(e => e.PlaceId).HasColumnName("PlaceID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlaceName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__96D4AAF74815B8DD");

            entity.Property(e => e.StaffId).HasColumnName("StaffID");
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
            entity.HasKey(e => e.TourId).HasName("PK__Tour__604CEA10B19D8111");

            entity.ToTable("Tour");

            entity.Property(e => e.TourId).HasColumnName("TourID");
            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.CouponCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ExpectedProfit).HasColumnType("money");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SingleRoomCost).HasColumnType("money");
            entity.Property(e => e.Tax).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TourGuide)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TourName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.CouponCodeNavigation).WithMany(p => p.Tours)
                .HasForeignKey(d => d.CouponCode)
                .HasConstraintName("FK__Tour__CouponCode__2A4B4B5E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
