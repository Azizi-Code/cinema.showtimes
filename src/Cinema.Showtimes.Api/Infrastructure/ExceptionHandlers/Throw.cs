namespace Cinema.Showtimes.Api.Infrastructure.ExceptionHandlers;

public static class Throw
{
    public static ArgumentExceptionHandler ArgumentException => new ArgumentExceptionHandler();
    public static ArgumentNullExceptionHandler ArgumentNullException => new ArgumentNullExceptionHandler();
}