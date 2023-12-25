using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Domain.Exceptions;

public class NotContiguousSeatsException() : UnprocessableEntityException("Selected seats are not contiguous.");