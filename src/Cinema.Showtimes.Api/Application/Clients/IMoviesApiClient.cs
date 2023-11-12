using ProtoDefinitions;

namespace Cinema.Showtimes.Api.Application.Clients;

public interface IMoviesApiClient
{
    Task<showListResponse> GetAllAsync();
    Task<showResponse?> GetByIdAsync(string id);
}