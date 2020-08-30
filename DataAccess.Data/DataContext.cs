using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            ConfigureModelBuilderForUser(modelBuilder);
        }

        void ConfigureModelBuilderForUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(loc => loc.LocationId)
                .HasMaxLength(20)
                .IsRequired();

                entity.HasKey(loc => loc.LocationId);

                entity.Property(loc => loc.DisplayName).IsRequired();
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(team => team.TeamId)
                .HasMaxLength(20)
                .IsRequired();

                entity.HasKey(team => team.TeamId);

                entity.Property(team => team.RegisteredAt).HasColumnType("timestamp with time zone");

                entity.Property(team => team.LastUpdatedAt).HasColumnType("timestamp with time zone");

                entity.Property(team => team.SecretToken).IsRequired();

                entity.HasOne(team => team.LocationNav)
                      .WithOne(loc => loc.Team)
                      .HasForeignKey<Team>(team => team.Location)
                      .HasConstraintName("FK_Team_Loc");
            });

            modelBuilder.Entity<User>(entity => 
            {
                entity.HasKey(user => user.UserId);

                entity.Property(user => user.AddedAt).HasColumnType("timestamp with time zone");

                entity.Property(user => user.UserId)
                .HasMaxLength(60)
                .IsRequired();

                entity.Property(user => user.TeamId)
                .HasMaxLength(60)
                .IsRequired();

                entity.HasOne(p => p.Team).WithMany(d => d.Users).HasConstraintName("FK_User_Team").HasForeignKey(d => d.TeamId);
            });
        }
    }
}
