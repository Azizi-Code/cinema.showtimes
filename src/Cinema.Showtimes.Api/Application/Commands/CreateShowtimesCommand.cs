using Cinema.Showtimes.Api.Application.Requests;
using Cinema.Showtimes.Api.Application.Responses;
using Cinema.Showtimes.Api.Infrastructure.ExceptionHandlers;
using MediatR;

namespace Cinema.Showtimes.Api.Application.Commands;

public class CreateShowtimesCommand(int auditoriumId, int movieId, DateTime sessionDate)
    : IRequest, IRequest<CreateShowtimeResponse>
{
    public int AuditoriumId { get; } = auditoriumId;
    public int MovieId { get; } = movieId;
    public DateTime SessionDate { get; } = Throw.ArgumentNullException.IfNull(sessionDate, nameof(sessionDate));
}