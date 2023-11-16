namespace Cinema.Showtimes.Api.Domain.Entities;

public class ShowtimeEntity
{
    public int Id { get; private set; }
    public MovieEntity Movie { get; private set; }
    public DateTime SessionDate { get; private set; }
    public int AuditoriumId { get; private set; }
    public ICollection<TicketEntity> Tickets { get; private set; }

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