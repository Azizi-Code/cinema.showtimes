namespace Cinema.Showtimes.Api.Application.Requests;

public class CreateShowtimeRequest(int auditoriumId, int movieId, DateTime sessionDate)
{
    public int AuditoriumId { get; } = auditoriumId;
    public int MovieId { get; } = movieId;
    public DateTime SessionDate { get; } = sessionDate;
}