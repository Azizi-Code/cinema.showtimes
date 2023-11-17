namespace Cinema.Showtimes.Api.Application.Requests;

public class CreateShowtimeResponse
{
    public CreateShowtimeResponse(int showTimeId)
    {
        ShowTimeId = showTimeId;
    }

    public int ShowTimeId { get; }
}