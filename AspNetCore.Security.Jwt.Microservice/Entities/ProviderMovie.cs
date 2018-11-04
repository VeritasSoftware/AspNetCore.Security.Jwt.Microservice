namespace AspNetCore.Security.Jwt.Microservice.Entities
{
    /// <summary>
    /// Class ProviderMovie
    /// </summary>
    public class ProviderMovie
    {
        Provider _movieProvider;
        MovieDetails _movie;

        public Provider Provider => _movieProvider;
        public MovieDetails Movie => _movie;

        public ProviderMovie(Provider movieProvider, MovieDetails movie)
        {
            _movieProvider = movieProvider;
            _movie = movie;
        }
    }
}
