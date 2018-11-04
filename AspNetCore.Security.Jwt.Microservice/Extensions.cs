using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using AspNetCore.Security.Jwt.Microservice.Repository;
using AspNetCore.Security.Jwt.Microservice.Repository.Clients;
using AspNetCore.Security.Jwt.Microservice.Repository.Providers;

namespace AspNetCore.Security.Jwt.Microservice
{
    public static class Extensions
    {
        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            //Get settings from appSettings
            Settings settings = new Settings();
            configuration.GetSection("Settings").Bind(settings);

            var token = settings.Token;
            var noOfRetries = settings.NoOfRetries;
            var cacheDurationInHours = settings.CacheDurationInHours;
            var cinemaWorldUrl = settings.BaseProviderUrl + settings.Providers[Entities.Provider.cinemaworld].Url;
            var filmWorldUrl = settings.BaseProviderUrl + settings.Providers[Entities.Provider.filmworld].Url;

            //Add memory cache
            services.AddMemoryCache();

            //Set up dependency injection
            services.AddScoped<IMovieProviderClient>(p => new MovieProviderClient(token, noOfRetries))
                    .AddScoped<ICacheProvider>(p => new CacheProvider(p.GetService<IMemoryCache>(), cacheDurationInHours))
                    .AddScoped(p => new List<IMovieProvider>()
                                        {
                                            new CinemaWorldProvider(
                                                                        cinemaWorldUrl,
                                                                        p.GetService<IMovieProviderClient>(),
                                                                        p.GetService<ICacheProvider>()
                                                                    ),
                                            new FilmWorldProvider(
                                                                    filmWorldUrl,
                                                                    p.GetService<IMovieProviderClient>(),
                                                                    p.GetService<ICacheProvider>()
                                                                 )
                                        }
                              )
                    .AddScoped<IMovieRepository, MovieRepository>();

            services.BuildServiceProvider();
        }
    }
}
