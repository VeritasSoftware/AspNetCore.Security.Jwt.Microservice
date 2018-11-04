using AspNetCore.Security.Jwt.Microservice.Repository.Clients;

namespace AspNetCore.Security.Jwt.Microservice.Repository.Providers
{
    /// <summary>
    /// FilmWorldProvider
    /// </summary>
    public class FilmWorldProvider : MovieProviderBase
    {
        public FilmWorldProvider(string url, IMovieProviderClient moviesProviderClient,
                                             ICacheProvider cacheProvidert) 
            : base(url, Entities.Provider.filmworld, moviesProviderClient, cacheProvidert)
        {

        }
    }
}
