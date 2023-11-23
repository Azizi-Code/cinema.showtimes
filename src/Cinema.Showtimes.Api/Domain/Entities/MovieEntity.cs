using Cinema.Showtimes.Api.Infrastructure.ExceptionHandlers;

namespace Cinema.Showtimes.Api.Domain.Entities;

public class MovieEntity
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string ImdbId { get; private set; }
    public string Stars { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public IList<ShowtimeEntity> Showtimes { get; private set; }


    public MovieEntity(int id, string title, string imdbId, string stars, DateTime releaseDate)
    {
        Id = id;
        Title = Throw.ArgumentException.IfNullOrWhiteSpace(title, nameof(title));
        ImdbId = imdbId;
        Stars = stars;
        ReleaseDate = releaseDate;
    }

    private MovieEntity()
    {
    }
}