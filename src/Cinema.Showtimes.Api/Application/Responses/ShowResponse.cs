namespace Cinema.Showtimes.Api.Application.Responses;

public class ShowResponse(string id, string title, string year, string rate)
{
    public string Id { get; } = id;
    public string Title { get; } = title;
    public string Year { get; } = year;
    public string Rate { get; } = rate;
}