using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Domain.Exceptions;

public class SeatsSoldOutException() : UnprocessableEntityException("One ore more selected seats are soldOut.");