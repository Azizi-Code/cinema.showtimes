using Cinema.Showtimes.Api.Application.Caching;
using Cinema.Showtimes.Api.Application.Clients;
using Cinema.Showtimes.Api.Application.Constants;
using Cinema.Showtimes.Api.Application.Middlewares;
using Cinema.Showtimes.Api.Application.Services;
using Cinema.Showtimes.Api.Domain.Repositories;
using Cinema.Showtimes.Api.Infrastructure.ActionResults;
using Cinema.Showtimes.Api.Infrastructure.Caching;
using Cinema.Showtimes.Api.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

namespace Cinema.Showtimes.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IShowtimesRepository, ShowtimesRepository>();
        services.AddTransient<ITicketsRepository, TicketsRepository>();
        services.AddTransient<IAuditoriumsRepository, AuditoriumsRepository>();
        services.AddScoped<IMoviesApiClient, MoviesApiClientGrpc>();
        services.AddSingleton<IConnectionMultiplexer>(x =>
            ConnectionMultiplexer.Connect(Configuration.GetValue<string>(ApplicationConstant.RedisConnectionKey) ??
                                          throw new ApplicationException("Redis connection didn't set properly.")));
        services.AddSingleton<ICacheService, RedisCacheService>();
        services.AddScoped<IMoviesService, MoviesService>();
        services.AddSingleton<IActionResultProvider, ActionResultProvider>();
        services.AddSingleton(typeof(IActionResultMapper<>), typeof(ActionResultMapper<>));

        services.AddDbContext<CinemaContext>(options =>
        {
            options.UseInMemoryDatabase("CinemaDb")
                .EnableSensitiveDataLogging()
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        });
        services.AddControllers();

        services.AddHttpClient();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo { Title = "Cinema.Showtimes.Api", Version = "v1" });
        });

        services.AddLogging();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<LogRequestTimeMiddleware>();

        app.UseSwagger();
        app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Cinema.Showtimes.Api"); });

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


        SampleData.Initialize(app);
    }
}