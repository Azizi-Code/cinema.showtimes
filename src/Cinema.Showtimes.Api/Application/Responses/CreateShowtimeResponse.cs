namespace Cinema.Showtimes.Api.Application.Responses;

public class CreateShowtimeResponse
{
    public CreateShowtimeResponse(int showTimeId)
    {
        ShowTimeId = showTimeId;
    }

    public int ShowTimeId { get; }
}