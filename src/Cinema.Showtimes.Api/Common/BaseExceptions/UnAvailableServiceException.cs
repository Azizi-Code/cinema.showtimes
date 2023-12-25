namespace Cinema.Showtimes.Api.Common.BaseExceptions;

public class UnAvailableServiceException(string message) : Exception(message);