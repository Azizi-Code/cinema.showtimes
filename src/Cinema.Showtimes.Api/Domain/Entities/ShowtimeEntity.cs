namespace Cinema.Showtimes.Api.Domain.Entities;

public class ShowtimeEntity
{
    public int Id { get; }
    public MovieEntity Movie { get; }
    public DateTime SessionDate { get; }
    public int AuditoriumId { get; }
    public ICollection<TicketEntity> Tickets { get; }

    private ShowtimeEntity()
    {
    }

    public ShowtimeEntity(MovieEntity movie, DateTime sessionDate, int auditoriumId)
    {
        Movie = movie;
        SessionDate = sessionDate;
        AuditoriumId = auditoriumId;
    }

    public ShowtimeEntity(int id, MovieEntity movie, DateTime sessionDate, int auditoriumId) : this(movie, sessionDate,
        auditoriumId) => Id = id;

    public static ShowtimeEntity Create(MovieEntity movieEntity, DateTime sessionDate, int auditorium) =>
        new(movieEntity, sessionDate, auditorium);
}