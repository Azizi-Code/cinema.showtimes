using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Domain.Exceptions;

public class ReservationAlreadyPaidException()
    : UnprocessableEntityException("This reservation has already been paid.");