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
        public DbSet<Game> Games { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<RoundConfig> RoundConfigs { get; set; }
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
            modelBuilder.Entity<Game>(entity => 
            {
                entity.Property(game => game.Id).ValueGeneratedOnAdd();

                entity.HasKey(game => game.GameId);
                
                entity.Property(game => game.TimeStamp).HasColumnType("timestamp with time zone").IsRequired();
            });

            modelBuilder.Entity<Phase>(entity => 
            {
                entity.Property(phase => phase.Id).ValueGeneratedOnAdd();

                entity.HasKey(phase => new { phase.RoundId, phase.PhaseType });

                entity.Property(phase => phase.GameId).IsRequired();
                
                entity.Property(phase => phase.RoundId).IsRequired();

                entity.Property(phase => phase.PhaseType).HasConversion(
                    type => type.ToString(), // code to db
                    type => (PhaseType)Enum.Parse(typeof(PhaseType), type) // db to code
                );

                entity.Property(phase => phase.TimeStamp).HasColumnType("timestamp with time zone").IsRequired();

                entity.HasOne(phase => phase.Game)
                    .WithMany(game => game.Phases)
                    .HasForeignKey(phase => phase.GameId)
                    .HasConstraintName("FK__Phase__Game");

                entity.HasOne(phase => phase.Round)
                    .WithMany(round => round.RoundPhases)
                    .HasForeignKey(phase => phase.RoundId)
                    .HasConstraintName("FK__Phase__Round");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(loc => loc.LocationId)
                .HasMaxLength(20)
                .IsRequired();

                entity.HasKey(loc => loc.LocationId);

                entity.Property(loc => loc.DisplayName).IsRequired();

                entity.Property(loc => loc.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Round>(entity => 
            {
                entity.Property(round => round.Id).ValueGeneratedOnAdd();

                entity.HasKey(round => round.RoundId);

                entity.Property(round => round.TimeStamp).HasColumnType("timestamp with time zone").IsRequired();

                entity.Property(round => round.RoundNumber).IsRequired();

                entity.HasOne(round => round.Game)
                    .WithMany(game => game.Rounds)
                    .HasForeignKey(round => round.GameId)
                    .HasConstraintName("FK__Round__Game");
            });

            modelBuilder.Entity<RoundConfig>(entity => 
            {
                entity.HasKey(rc => rc.Id);
                entity.Property(rc => rc.Id).HasColumnName("RoundNumber");
                entity.Property(rc => rc.JoiningDuration).IsRequired();
                entity.Property(rc => rc.RunningDuration).IsRequired();
                entity.Property(rc => rc.FinishedDuration).IsRequired();
                entity.Property(rc => rc.Penalty).IsRequired();
                entity.Property(rc => rc.DefaultLives).IsRequired();
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
                      .WithMany(loc => loc.Teams)
                      .HasForeignKey(team => team.Location)
                      .HasConstraintName("FK_Team_Loc");

                entity.Property(loc => loc.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<User>(entity => 
            {
                entity.HasKey(user => user.UserId);

                entity.Property(user => user.AddedAt).HasColumnType("timestamp with time zone");

                entity.Property(loc => loc.Id).ValueGeneratedOnAdd();

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
