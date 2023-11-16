namespace Cinema.Showtimes.Api.Application.Requests;

public class CreateShowtimeRequest
{
    public int AuditoriumId { get; }
    public int MovieId { get; }
    public DateTime SessionDate { get; }

    public CreateShowtimeRequest(int auditoriumId, int movieId, DateTime sessionDate)
    {
        AuditoriumId = auditoriumId;
        MovieId = movieId;
        SessionDate = sessionDate;
    }
}