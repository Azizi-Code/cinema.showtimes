using Cinema.Showtimes.Api.Application.Requests;
using Cinema.Showtimes.Api.Infrastructure.ExceptionHandlers;
using MediatR;

namespace Cinema.Showtimes.Api.Application.Commands;

public class CreateShowtimesCommand : IRequest, IRequest<CreateShowtimeResponse>
{
    public int AuditoriumId { get; }
    public int MovieId { get; }
    public DateTime SessionDate { get; }

    public CreateShowtimesCommand(int auditoriumId, int movieId, DateTime sessionDate)
    {
        AuditoriumId = auditoriumId;
        MovieId = movieId;
        SessionDate = Throw.ArgumentNullException.IfNull(sessionDate, nameof(sessionDate));
    }
}