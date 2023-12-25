namespace Cinema.Showtimes.Api.Common.BaseExceptions;

public class UnprocessableEntityException(string message) : Exception(message);