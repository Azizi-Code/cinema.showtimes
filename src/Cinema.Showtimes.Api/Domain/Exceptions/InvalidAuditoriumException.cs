using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Domain.Exceptions;

public class InvalidAuditoriumException()
    : UnprocessableEntityException("The selected auditorium does not match the show's auditorium.");