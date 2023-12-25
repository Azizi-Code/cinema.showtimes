using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Domain.Exceptions;

public class SeatsAlreadyReservedException()
    : UnprocessableEntityException("One or more selected seats are already reserved.");