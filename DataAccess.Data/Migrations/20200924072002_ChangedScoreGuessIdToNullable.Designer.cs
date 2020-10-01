﻿// <auto-generated />
using System;
using DataAccess.Data;
using DataAccess.Model.SharedModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200924072002_ChangedScoreGuessIdToNullable")]
    partial class ChangedScoreGuessIdToNullable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("DataAccess.Model.FutureGame", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("RunGamesTill")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(null);

                    b.Property<bool>("RunOnce")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("FutureGames");
                });

            modelBuilder.Entity("DataAccess.Model.Game", b =>
                {
                    b.Property<Guid>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("DataAccess.Model.Guess", b =>
                {
                    b.Property<Guid>("GuessId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uuid");

                    b.Property<GuessRequestBody>("GuessRequest")
                        .HasColumnType("jsonb");

                    b.Property<GuessResponseBody>("GuessResponse")
                        .HasColumnType("jsonb");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<Guid>("RoundId")
                        .HasColumnType("uuid");

                    b.Property<string>("TeamId")
                        .IsRequired()
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("GuessId");

                    b.HasIndex("GameId");

                    b.HasIndex("RoundId");

                    b.HasIndex("TeamId");

                    b.ToTable("Guesses");
                });

            modelBuilder.Entity("DataAccess.Model.Kill", b =>
                {
                    b.Property<Guid>("RoundId")
                        .HasColumnType("uuid");

                    b.Property<string>("VictimId")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<string>("KillerId")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<Guid>("GameId")
                        .HasColumnType("uuid");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("RoundId", "VictimId", "KillerId");

                    b.HasIndex("GameId");

                    b.HasIndex("KillerId");

                    b.HasIndex("VictimId");

                    b.ToTable("Kills");
                });

            modelBuilder.Entity("DataAccess.Model.Location", b =>
                {
                    b.Property<string>("LocationId")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("DataAccess.Model.Participant", b =>
                {
                    b.Property<Guid>("RoundId")
                        .HasColumnType("uuid");

                    b.Property<string>("TeamId")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<Guid>("GameId")
                        .HasColumnType("uuid");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool?>("IsAlive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("JoinedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("RoundId", "TeamId");

                    b.HasIndex("GameId");

                    b.HasIndex("TeamId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("DataAccess.Model.Phase", b =>
                {
                    b.Property<Guid>("RoundId")
                        .HasColumnType("uuid");

                    b.Property<string>("PhaseType")
                        .HasColumnType("text");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uuid");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("RoundId", "PhaseType");

                    b.HasIndex("GameId");

                    b.ToTable("Phases");
                });

            modelBuilder.Entity("DataAccess.Model.Request", b =>
                {
                    b.Property<Guid>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValue(null);

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("RequestApi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RequestMethod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("RoundId")
                        .HasColumnType("uuid")
                        .HasDefaultValue(null);

                    b.Property<int>("StatusCode")
                        .HasColumnType("integer");

                    b.Property<string>("TeamId")
                        .HasColumnType("character varying(20)")
                        .HasDefaultValue(null);

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("RequestId");

                    b.HasIndex("GameId");

                    b.HasIndex("RoundId");

                    b.HasIndex("TeamId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("DataAccess.Model.Round", b =>
                {
                    b.Property<Guid>("RoundId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uuid");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("RoundNumber")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("RoundId");

                    b.HasIndex("GameId");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("DataAccess.Model.RoundConfig", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RoundNumber")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("FinishedDuration")
                        .HasColumnType("bigint");

                    b.Property<long>("JoiningDuration")
                        .HasColumnType("bigint");

                    b.Property<int>("LifeLines")
                        .HasColumnType("integer");

                    b.Property<long>("Penalty")
                        .HasColumnType("bigint");

                    b.Property<long>("RunningDuration")
                        .HasColumnType("bigint");

                    b.Property<int>("SecretLength")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("RoundConfigs");
                });

            modelBuilder.Entity("DataAccess.Model.Score", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<Guid>("GameId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("GuessId")
                        .HasColumnType("uuid");

                    b.Property<long>("PointsScored")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L);

                    b.Property<Guid>("RoundId")
                        .HasColumnType("uuid");

                    b.Property<string>("TeamId")
                        .IsRequired()
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("GuessId")
                        .IsUnique();

                    b.HasIndex("RoundId");

                    b.HasIndex("TeamId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("DataAccess.Model.Team", b =>
                {
                    b.Property<string>("TeamId")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsRobot")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Location")
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime>("RegisteredAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecretToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TeamId");

                    b.HasIndex("Location");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("DataAccess.Model.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("character varying(60)")
                        .HasMaxLength(60);

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("TeamId")
                        .IsRequired()
                        .HasColumnType("character varying(60)")
                        .HasMaxLength(60);

                    b.HasKey("UserId");

                    b.HasIndex("TeamId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataAccess.Model.Guess", b =>
                {
                    b.HasOne("DataAccess.Model.Game", "Game")
                        .WithMany("Guesses")
                        .HasForeignKey("GameId")
                        .HasConstraintName("FK__Guess__Game")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.Round", "Round")
                        .WithMany("Guesses")
                        .HasForeignKey("RoundId")
                        .HasConstraintName("FK__Guess__Round")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.Team", "Team")
                        .WithMany("Guesses")
                        .HasForeignKey("TeamId")
                        .HasConstraintName("FK__Guess__Team")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Model.Kill", b =>
                {
                    b.HasOne("DataAccess.Model.Game", "Game")
                        .WithMany("Kills")
                        .HasForeignKey("GameId")
                        .HasConstraintName("FK__Kill__Game")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.Team", "Killer")
                        .WithMany("KillRecord")
                        .HasForeignKey("KillerId")
                        .HasConstraintName("FK__Kill__Killer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.Round", "Round")
                        .WithMany("Kills")
                        .HasForeignKey("RoundId")
                        .HasConstraintName("FK__Kill__Round")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.Team", "Victim")
                        .WithMany("DeathRecord")
                        .HasForeignKey("VictimId")
                        .HasConstraintName("FK__Kill__Victim")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Model.Participant", b =>
                {
                    b.HasOne("DataAccess.Model.Game", "Game")
                        .WithMany("Participants")
                        .HasForeignKey("GameId")
                        .HasConstraintName("FK__Part__Game")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.Round", "Round")
                        .WithMany("Participants")
                        .HasForeignKey("RoundId")
                        .HasConstraintName("FK__Part__Round")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.Team", "Team")
                        .WithMany("ParticipationRecord")
                        .HasForeignKey("TeamId")
                        .HasConstraintName("FK__Part__Team")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Model.Phase", b =>
                {
                    b.HasOne("DataAccess.Model.Game", "Game")
                        .WithMany("Phases")
                        .HasForeignKey("GameId")
                        .HasConstraintName("FK__Phase__Game")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.Round", "Round")
                        .WithMany("RoundPhases")
                        .HasForeignKey("RoundId")
                        .HasConstraintName("FK__Phase__Round")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Model.Request", b =>
                {
                    b.HasOne("DataAccess.Model.Game", "Game")
                        .WithMany("Requests")
                        .HasForeignKey("GameId")
                        .HasConstraintName("FK__Req__Game")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataAccess.Model.Round", "Round")
                        .WithMany("Requests")
                        .HasForeignKey("RoundId")
                        .HasConstraintName("FK__Req__Round")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataAccess.Model.Team", "Team")
                        .WithMany("Requests")
                        .HasForeignKey("TeamId")
                        .HasConstraintName("FK__Req__Team")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataAccess.Model.Round", b =>
                {
                    b.HasOne("DataAccess.Model.Game", "Game")
                        .WithMany("Rounds")
                        .HasForeignKey("GameId")
                        .HasConstraintName("FK__Round__Game")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Model.Score", b =>
                {
                    b.HasOne("DataAccess.Model.Game", "Game")
                        .WithMany("Scores")
                        .HasForeignKey("GameId")
                        .HasConstraintName("FK__Score__Game")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.Guess", "Guess")
                        .WithOne("Score")
                        .HasForeignKey("DataAccess.Model.Score", "GuessId")
                        .HasConstraintName("FK__Score__Guess")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataAccess.Model.Round", "Round")
                        .WithMany("Scores")
                        .HasForeignKey("RoundId")
                        .HasConstraintName("FK__Score__Round")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.Team", "Team")
                        .WithMany("Scores")
                        .HasForeignKey("TeamId")
                        .HasConstraintName("FK__Score__Team")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Model.Team", b =>
                {
                    b.HasOne("DataAccess.Model.Location", "LocationNav")
                        .WithMany("Teams")
                        .HasForeignKey("Location")
                        .HasConstraintName("FK_Team_Loc")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataAccess.Model.User", b =>
                {
                    b.HasOne("DataAccess.Model.Team", "Team")
                        .WithMany("Users")
                        .HasForeignKey("TeamId")
                        .HasConstraintName("FK_User_Team")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
