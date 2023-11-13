namespace Cinema.Showtimes.Api.Domain.Entities;

public class MovieEntity
{
    public int Id { get; }
    public string Title { get; }
    public string ImdbId { get; }
    public string Stars { get; }
    public DateTime ReleaseDate { get; }
    public IList<ShowtimeEntity> Showtimes { get; }

    private MovieEntity()
    {
    }

    public MovieEntity(string title, string imdbId, string stars, DateTime releaseDate)
    {
        Title = title;
        ImdbId = imdbId;
        Stars = stars;
        ReleaseDate = releaseDate;
    }

    public MovieEntity(int id, string title, string imdbId, string stars, DateTime releaseDate) : this(title, imdbId,
        stars, releaseDate) => Id = id;
}