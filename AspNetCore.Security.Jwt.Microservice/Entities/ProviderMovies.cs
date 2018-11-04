using System.Collections.Generic;

namespace AspNetCore.Security.Jwt.Microservice.Entities
{
    /// <summary>
    /// Class ProviderMovies
    /// </summary>
    public class ProviderMovies
    {
        private Provider _movieProvider;

        public Provider Name => _movieProvider;
        public List<Movie> Movies { get; set; }

        public ProviderMovies(Provider movieProvider)
        {
            _movieProvider = movieProvider;
            this.Movies = new List<Movie>();
        }
    }
}
