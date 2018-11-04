namespace AspNetCore.Security.Jwt.Microservice.Repository.Providers
{
    using AspNetCore.Security.Jwt.Microservice.Entities;

    /// <summary>
    /// Interface IMovieProvider
    /// </summary>
    public interface IMovieProvider
    {
        Provider Name { get; }

        ProviderMovies GetMovies();
        ProviderMovie GetMovie(string id);
    }
}
