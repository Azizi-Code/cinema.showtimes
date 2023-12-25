﻿using Cinema.Showtimes.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Showtimes.Api.Infrastructure.Database;

public class CinemaContext(DbContextOptions<CinemaContext> options) : DbContext(options)
{
    public DbSet<AuditoriumEntity> Auditoriums { get; set; }
    public DbSet<ShowtimeEntity> ShowTimes { get; set; }
    public DbSet<MovieEntity> Movies { get; set; }
    public DbSet<TicketEntity> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditoriumEntity>(build =>
        {
            build.HasKey(entry => entry.Id);
            build.Property(entry => entry.Id).ValueGeneratedOnAdd();
            build.HasMany(entry => entry.Showtimes).WithOne().HasForeignKey(entity => entity.AuditoriumId);
        });

        modelBuilder.Entity<SeatEntity>(build =>
        {
            build.HasKey(entry => new { entry.AuditoriumId, entry.Row, entry.SeatNumber });
            build.HasOne(entry => entry.Auditorium).WithMany(entry => entry.Seats)
                .HasForeignKey(entry => entry.AuditoriumId);
        });

        modelBuilder.Entity<ShowtimeEntity>(build =>
        {
            build.HasKey(entry => entry.Id);
            build.Property(entry => entry.Id).ValueGeneratedOnAdd();
            build.HasOne(entry => entry.Movie).WithMany(entry => entry.Showtimes);
            build.HasMany(entry => entry.Tickets).WithOne(entry => entry.Showtime)
                .HasForeignKey(entry => entry.ShowtimeId);
        });

        modelBuilder.Entity<MovieEntity>(build =>
        {
            build.HasKey(entry => entry.Id);
            build.Property(entry => entry.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TicketEntity>(build =>
        {
            build.HasKey(entry => entry.Id);
            build.Property(entry => entry.Id).ValueGeneratedOnAdd();
        });
    }
}