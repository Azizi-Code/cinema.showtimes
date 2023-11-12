using Cinema.Showtimes.Api.Application.Constants;
using Grpc.Core;
using Grpc.Net.Client;
using ProtoDefinitions;

namespace Cinema.Showtimes.Api.Application.Clients;

public class MoviesApiClientGrpc : IMoviesApiClient
{
    private readonly IConfiguration _configuration;

    public MoviesApiClientGrpc(IConfiguration configuration) => _configuration = configuration;

    public async Task<showListResponse> GetAllAsync()
    {
        var moviesApiClient = GetMoviesApiClient();
        var apiKeyHeader = GetApiKeyHeader();

        var response = await moviesApiClient.GetAllAsync(new Empty(), apiKeyHeader);
        if (response.Success)
        {
            response.Data.TryUnpack<showListResponse>(out var data);
            return data;
        }

        return null;
    }

    public async Task<showResponse?> GetByIdAsync(string movieId)
    {
        try
        {
            var moviesApiClient = GetMoviesApiClient();
            var apiKeyHeader = GetApiKeyHeader();

            var response = await moviesApiClient.GetByIdAsync(new IdRequest { Id = movieId }, apiKeyHeader);
            
            if (!response.Success) return null;
            response.Data.TryUnpack<showResponse>(out var data);
            return data;
        }
        catch
        {
            return null;
        }
    }

    private MoviesApi.MoviesApiClient GetMoviesApiClient()
    {
        using var httpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        using var channel = GrpcChannel.ForAddress(ApplicationConstant.MovieApiAddress, new GrpcChannelOptions
        {
            HttpHandler = httpHandler
        });

        return new MoviesApi.MoviesApiClient(channel);
    }

    private Metadata GetApiKeyHeader()
    {
        var apiKeyHeaderValue = _configuration.GetValue<string>(ApplicationConstant.ApiKeyName);

        return apiKeyHeaderValue != null
            ? new Metadata { { ApplicationConstant.ApiKeyHeaderName, apiKeyHeaderValue } }
            : throw new ArgumentNullException();
    }
}