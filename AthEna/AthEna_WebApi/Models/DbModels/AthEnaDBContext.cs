using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AthEna_WebApi.Models
{
    public partial class AthEnaDBContext : DbContext
    {
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<MetroStation> MetroStations { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<ValidationActivity> ValidationActivity { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=83.212.102.241; Database=AthEnaDB; User Id=kapoios; Password=qwerty123456!@#$%^;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(e => e.CardId);

                entity.Property(e => e.CardId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ChargeExpiresOn).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Id).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

                entity.Property(e => e.LastRechargedOn).HasColumnType("datetime");

                entity.Property(e => e.RegisteredOn).HasColumnType("datetime");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cards_Contacts");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.Property(e => e.ContactId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IdCardNum)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<MetroStation>(entity =>
            {
                entity.HasKey(e => e.StationId);

                entity.Property(e => e.StationId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IsAlsoOnLine).HasColumnName("isAlsoOnLine");

                entity.Property(e => e.IsOnLine).HasColumnName("isOnLine");

                entity.Property(e => e.StationName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.HasKey(e => e.RouteId);

                entity.Property(e => e.RouteId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.RouteCodeNum)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.RouteEndPoint)
                    .IsRequired()
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.RouteStartPoint)
                    .IsRequired()
                    .HasColumnType("nchar(50)");
            });

            modelBuilder.Entity<ValidationActivity>(entity =>
            {
                entity.HasKey(e => e.VactivityId);

                entity.Property(e => e.VactivityId)
                    .HasColumnName("VActivityId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ValidatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Bus)
                    .WithMany(p => p.ValidationActivity)
                    .HasForeignKey(d => d.BusId)
                    .HasConstraintName("FK_ValidationActivity_Vehicles");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.ValidationActivity)
                    .HasForeignKey(d => d.CardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ValidationActivity_Cards");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.ValidationActivity)
                    .HasForeignKey(d => d.RouteId)
                    .HasConstraintName("FK_ValidationActivity_Routes");

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.ValidationActivity)
                    .HasForeignKey(d => d.StationId)
                    .HasConstraintName("FK_ValidationActivity_MetroStations");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.VehicleId);

                entity.Property(e => e.VehicleId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LicensePlate)
                    .IsRequired()
                    .HasMaxLength(10);
            });
        }
    }
}
