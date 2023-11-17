using Cinema.Showtimes.Api.Application.Responses;
using ProtoDefinitions;

namespace Cinema.Showtimes.Api.Application.Mappers;

public static class ShowResponseMapper
{
    public static ShowResponse MapToResponse(this showResponse response)
        => new ShowResponse(response.Id, response.Title, response.Year, response.ImDbRating);
}