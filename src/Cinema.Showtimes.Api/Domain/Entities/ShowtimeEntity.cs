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

    public ShowtimeEntity(MovieEntity movie, DateTime sessionDate, int auditoriumId,
        ICollection<TicketEntity> tickets = default)
    {
        Movie = movie;
        SessionDate = sessionDate;
        AuditoriumId = auditoriumId;
        Tickets = tickets;
    }

    public ShowtimeEntity(int id, MovieEntity movie, DateTime sessionDate, int auditoriumId,
        ICollection<TicketEntity> tickets = default) : this(movie, sessionDate,
        auditoriumId, tickets) => Id = id;

    public static ShowtimeEntity Create(MovieEntity movieEntity, DateTime sessionDate, int auditorium) =>
        new(movieEntity, sessionDate, auditorium);
}