using Cinema.Showtimes.Api.Application.Constants;
using Cinema.Showtimes.Api.Application.Exceptions;
using Cinema.Showtimes.Api.Infrastructure.ExceptionHandlers;
using Grpc.Core;
using Grpc.Net.Client;
using ProtoDefinitions;

namespace Cinema.Showtimes.Api.Application.Clients;

public class MoviesApiClientGrpc : IMoviesApiClient
{
    private readonly IConfiguration _configuration;

    public MoviesApiClientGrpc(IConfiguration configuration) =>
        _configuration = Throw.ArgumentNullException.IfNull(configuration, nameof(configuration));

    public async Task<showListResponse> GetAllAsync()
    {
        try
        {
            var moviesApiClient = GetMoviesApiClient();
            var apiKeyHeader = GetApiKeyHeader();

            var response = await moviesApiClient.GetAllAsync(new Empty(), apiKeyHeader);

            response.Data.TryUnpack<showListResponse>(out var data);
            return data;
        }
        catch (Exception ex)
        {
            throw new MovieApiException($"process for fetching all movie data failed. Error message => {ex.Message}");
        }
    }

    public async Task<showResponse?> GetByIdAsync(string movieId)
    {
        try
        {
            var moviesApiClient = GetMoviesApiClient();
            var apiKeyHeader = GetApiKeyHeader();

            var response = await moviesApiClient.GetByIdAsync(new IdRequest { Id = movieId }, apiKeyHeader);

            response.Data.TryUnpack<showResponse>(out var data);
            return data;
        }
        catch (RpcException ex)
        {
            throw new MovieApiException($"process for fetching movie data failed. Error message => {ex.Message}");
        }
    }

    private MoviesApi.MoviesApiClient GetMoviesApiClient()
    {
        var httpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        var channel = GrpcChannel.ForAddress(ApplicationConstant.MovieApiAddress, new GrpcChannelOptions
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
            : throw new Exception("The value for ApiKey header can't be null.");
    }
}