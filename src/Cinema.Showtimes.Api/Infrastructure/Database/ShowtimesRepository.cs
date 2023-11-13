﻿using System.Linq.Expressions;
using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Showtimes.Api.Infrastructure.Database;

public class ShowtimesRepository : IShowtimesRepository
{
    private readonly CinemaContext _context;

    public ShowtimesRepository(CinemaContext context)
    {
        _context = context;
    }

    public async Task<ShowtimeEntity?> GetWithMoviesByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.ShowTimes
            .Include(x => x.Movie)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<ShowtimeEntity?> GetWithTicketsByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.ShowTimes
            .Include(x => x.Tickets)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ShowtimeEntity>?> GetAllAsync(Expression<Func<ShowtimeEntity, bool>>? filter,
        CancellationToken cancellationToken)
    {
        if (filter == null)
        {
            return await _context.ShowTimes
                .Include(x => x.Movie)
                .ToListAsync(cancellationToken);
        }

        return await _context.ShowTimes
            .Include(x => x.Movie)
            .Where(filter)
            .ToListAsync(cancellationToken);
    }

    public async Task<ShowtimeEntity> CreateShowtimeAsync(ShowtimeEntity showtimeEntity,
        CancellationToken cancellationToken)
    {
        var showtime = await _context.ShowTimes.AddAsync(showtimeEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return showtime.Entity;
    }
}