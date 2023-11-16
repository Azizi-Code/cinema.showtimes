using Cinema.Showtimes.Api.Domain.Entities;

namespace Cinema.Showtimes.Api.Domain.Repositories;

public interface IAuditoriumsRepository
{
    Task<AuditoriumEntity?> GetByIdAsync(int auditoriumId, CancellationToken cancellationToken);
    Task<AuditoriumEntity?> GetWithSeatsByIdAsync(int auditoriumId, CancellationToken cancellationToken);
}