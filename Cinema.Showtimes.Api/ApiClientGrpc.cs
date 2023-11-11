using Cinema.Showtimes.Api.Constants;
using Grpc.Core;
using Grpc.Net.Client;
using ProtoDefinitions;

namespace Cinema.Showtimes.Api
{
    public class MoviesApiClientGrpc : IMoviesApiClient
    {
        private readonly IConfiguration _configuration;

        public MoviesApiClientGrpc(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<showListResponse> GetAllAsync()
        {
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            var channel = GrpcChannel.ForAddress("https://localhost:7443", new GrpcChannelOptions
                {
                    HttpHandler = httpHandler
                });
            var client = new MoviesApi.MoviesApiClient(channel);
            var apiKeyHeaderValue = _configuration.GetValue<string>(ApplicationConstant.ApiKeyName);
            var headers = new Metadata { { ApplicationConstant.ApiKeyHeaderName, apiKeyHeaderValue} };
            var all = await client.GetAllAsync(new Empty(), headers);
            if (all.Success)
            {
                all.Data.TryUnpack<showListResponse>(out var data);
                return data;
            }
            return null;
        }
    }
}