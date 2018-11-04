using System.Collections.Generic;

namespace AspNetCore.Security.Jwt.Microservice.Entities
{
    /// <summary>
    /// Class Movie
    /// </summary>
    public class Movie
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string ID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }

    /// <summary>
    /// Class MoviesCollection
    /// </summary>
    public class MoviesCollection
    {
        public IEnumerable<Movie> Movies { get; set; }
    }
}
