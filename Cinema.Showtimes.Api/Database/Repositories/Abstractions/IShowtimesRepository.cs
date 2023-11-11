using System.Linq.Expressions;
using Cinema.Showtimes.Api.Database.Entities;

namespace Cinema.Showtimes.Api.Database.Repositories.Abstractions
{
    public interface IShowtimesRepository
    {
        Task<ShowtimeEntity> CreateShowtime(ShowtimeEntity showtimeEntity, CancellationToken cancel);
        Task<IEnumerable<ShowtimeEntity>> GetAllAsync(Expression<Func<ShowtimeEntity, bool>> filter, CancellationToken cancel);
        Task<ShowtimeEntity> GetWithMoviesByIdAsync(int id, CancellationToken cancel);
        Task<ShowtimeEntity> GetWithTicketsByIdAsync(int id, CancellationToken cancel);
    }
}