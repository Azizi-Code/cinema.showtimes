using Cinema.Showtimes.Api.Domain.Entities;

namespace Cinema.Showtimes.Api.Infrastructure.Database;

public class SampleData
{
    public static void Initialize(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetService<CinemaContext>();
        context.Database.EnsureCreated();


        context.Auditoriums.Add(new AuditoriumEntity
        (
            1,
            new List<ShowtimeEntity>
            {
                new ShowtimeEntity
                (
                    1,
                    new MovieEntity(
                        1,
                        "Inception",
                        "tt1375666",
                        "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page, Ken Watanabe",
                        new DateTime(2010, 01, 14)
                    )
                    ,
                    new DateTime(2023, 1, 1),
                    1
                )
            },
            GenerateSeats(1, 28, 22)
        ));

        context.Auditoriums.Add(new AuditoriumEntity(
            2,
            GenerateSeats(2, 21, 18)
        ));

        context.Auditoriums.Add(new AuditoriumEntity
        (
            3,
            GenerateSeats(3, 15, 21)
        ));

        context.SaveChanges();
    }

    private static List<SeatEntity> GenerateSeats(int auditoriumId, short rows, short seatsPerRow)
    {
        var seats = new List<SeatEntity>();
        for (short r = 1; r <= rows; r++)
        for (short s = 1; s <= seatsPerRow; s++)
            seats.Add(new SeatEntity(auditoriumId, r, s));

        return seats;
    }
}