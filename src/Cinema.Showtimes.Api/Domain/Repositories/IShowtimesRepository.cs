using System.Linq.Expressions;
using Cinema.Showtimes.Api.Domain.Entities;

namespace Cinema.Showtimes.Api.Domain.Repositories;

public interface IShowtimesRepository
{
    Task<ShowtimeEntity> CreateShowtimeAsync(ShowtimeEntity showtimeEntity, CancellationToken cancellationToken);

    Task<IEnumerable<ShowtimeEntity>?> GetAllAsync(Expression<Func<ShowtimeEntity, bool>>? filter,
        CancellationToken cancellationToken);

    Task<ShowtimeEntity?> GetWithMoviesByIdAsync(int id, CancellationToken cancellationToken);

    Task<ShowtimeEntity?> GetWithTicketsByIdAsync(int id, CancellationToken cancellationToken);
}