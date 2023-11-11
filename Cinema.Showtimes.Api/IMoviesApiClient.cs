using ProtoDefinitions;

namespace Cinema.Showtimes.Api;

public interface IMoviesApiClient
{
    Task<showListResponse> GetAllAsync();
}