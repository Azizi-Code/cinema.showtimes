using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Domain.Exceptions;

public class InvalidSeatsException() : UnprocessableEntityException("The selected seats are not valid.");