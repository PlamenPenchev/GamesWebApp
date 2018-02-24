using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GamesApp.Models
{
    public partial class GamesDBContext : DbContext
    {
        public virtual DbSet<Developers> Developers { get; set; }
        public virtual DbSet<GameDev> GameDev { get; set; }
        public virtual DbSet<Games> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-6UHGT1S;Database=GamesDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Developers>(entity =>
            {
                entity.HasKey(e => e.DeveloperId);

                entity.Property(e => e.DeveloperId)
                    .HasColumnName("DeveloperID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<GameDev>(entity =>
            {
                entity.Property(e => e.GameDevId)
                    .HasColumnName("GameDevID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DeveloperId).HasColumnName("DeveloperID");

                entity.Property(e => e.GameId).HasColumnName("GameID");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.HasOne(d => d.Developer)
                    .WithMany(p => p.GameDev)
                    .HasForeignKey(d => d.DeveloperId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GameDev_Developers");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameDev)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GameDev_Games");
            });

            modelBuilder.Entity<Games>(entity =>
            {
                entity.HasKey(e => e.GameId);

                entity.Property(e => e.GameId)
                    .HasColumnName("GameID")
                    .ValueGeneratedNever();

                

                entity.Property(e => e.Title).HasMaxLength(50);
            });
        }
    }
}
