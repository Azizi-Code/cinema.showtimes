using Cinema.Showtimes.Api.Database.Entities;

namespace Cinema.Showtimes.Api.Database.Repositories.Abstractions
{
    public interface IAuditoriumsRepository
    {
        Task<AuditoriumEntity> GetAsync(int auditoriumId, CancellationToken cancel);
    }
}