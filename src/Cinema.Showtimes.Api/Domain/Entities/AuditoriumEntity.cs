namespace Cinema.Showtimes.Api.Domain.Entities;

public class AuditoriumEntity
{
    public int Id { get; private set; }
    public IList<ShowtimeEntity>? Showtimes { get; private set; }
    public ICollection<SeatEntity> Seats { get; private set; }

    public AuditoriumEntity(ICollection<SeatEntity> seats, IList<ShowtimeEntity>? showtimes = default)
    {
        Showtimes = showtimes;
        Seats = seats;
    }

    public AuditoriumEntity(int id, ICollection<SeatEntity> seats, IList<ShowtimeEntity>? showtimes = default) : this(
        seats, showtimes) => Id = id;

    private AuditoriumEntity()
    {
    }
}