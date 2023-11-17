namespace Cinema.Showtimes.Api.Application.Responses;

public class ShowResponse
{
    public string Id { get; }
    public string Title { get; }
    public string Year { get; }
    public string Rate { get; }

    public ShowResponse(string id, string title, string year, string rate)
    {
        Id = id;
        Title = title;
        Year = year;
        Rate = rate;
    }
}