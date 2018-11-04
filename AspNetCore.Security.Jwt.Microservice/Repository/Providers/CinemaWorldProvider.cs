using AspNetCore.Security.Jwt.Microservice.Repository.Clients;

namespace AspNetCore.Security.Jwt.Microservice.Repository.Providers
{
    using AspNetCore.Security.Jwt.Microservice.Entities;

    /// <summary>
    /// Class CinemaWorldProvider
    /// </summary>
    public class CinemaWorldProvider : MovieProviderBase
    {
        public CinemaWorldProvider(string url, IMovieProviderClient moviesProviderClient, 
                                               ICacheProvider cacheProvider) 
            : base(url, Provider.cinemaworld, moviesProviderClient, cacheProvider)
        {

        }
    }
}
