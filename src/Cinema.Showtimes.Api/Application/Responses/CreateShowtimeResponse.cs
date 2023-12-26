namespace Cinema.Showtimes.Api.Application.Responses;

public class CreateShowtimeResponse(int showTimeId)
{
    public int ShowTimeId { get; } = showTimeId;
}