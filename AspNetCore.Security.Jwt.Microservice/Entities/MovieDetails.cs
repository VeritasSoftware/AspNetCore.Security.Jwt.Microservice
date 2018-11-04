namespace AspNetCore.Security.Jwt.Microservice.Entities
{
    /// <summary>
    /// Class MovieDetails
    /// </summary>
    public class MovieDetails : Movie
    {
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public string Metascore { get; set; }
        public string Rating { get; set; }
        public string Votes { get; set; }
        public string Price { get; set; }
    }
}
