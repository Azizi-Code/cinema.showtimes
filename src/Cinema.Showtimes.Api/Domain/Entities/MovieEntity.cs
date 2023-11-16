namespace Cinema.Showtimes.Api.Domain.Entities;

public class MovieEntity
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string ImdbId { get; private set; }
    public string Stars { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public IList<ShowtimeEntity> Showtimes { get; private set; }

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