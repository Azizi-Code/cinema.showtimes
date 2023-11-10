using ProtoDefinitions;

namespace ApiApplication;

public interface IMoviesApiClient
{
    Task<showListResponse> GetAll();
}