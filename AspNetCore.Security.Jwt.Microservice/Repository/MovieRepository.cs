using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Security.Jwt.Microservice.Entities;
using AspNetCore.Security.Jwt.Microservice.Repository.Providers;

namespace AspNetCore.Security.Jwt.Microservice.Repository
{
    /// <summary>
    /// Class MovieRepository
    /// </summary>
    public class MovieRepository : IMovieRepository
    {
        IEnumerable<IMovieProvider> _movieProviders;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="movieProviders">The injected movie providers</param>
        public MovieRepository(List<IMovieProvider> movieProviders)
        {
            _movieProviders = movieProviders;
        }

        /// <summary>
        /// Get cheapest deal for a movie from all providers
        /// </summary>
        /// <param name="title">The title words of the movie</param>
        /// <returns><see cref="Task{IEnumerable{ProviderMovie}}"/></returns>
        public async Task<IEnumerable<ProviderMovie>> GetCheapestDeal(string title)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var moviesFromAllProviders = new ConcurrentBag<ProviderMovies>();

                    //Get all movies
                    Parallel.ForEach(_movieProviders, provider =>
                    {
                        try
                        {
                            var movies = provider.GetMovies();

                            if(movies != null)
                            {
                                moviesFromAllProviders.Add(movies);
                            }                            

                            Console.WriteLine($"Successfully got movies from Provider {provider.Name.ToString()}.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to get movies from Provider {provider.Name.ToString()}. {ex.Message}");
                        }
                    });

                    //Get movies that match the title
                    var foundMoviesFromProviders = moviesFromAllProviders.Select(providerMovies => {
                        var p = new ProviderMovies(providerMovies.Name);

                        p.Movies.AddRange(providerMovies.Movies.Where(m => m.Title.ToLower().Contains(title.ToLower())));

                        return p;
                    });

                    var movieDetailsFromProviders = new ConcurrentBag<ProviderMovie>();

                    //Get all movie details for found movies
                    Parallel.ForEach(foundMoviesFromProviders, providerMovies =>
                    {
                        Parallel.ForEach(providerMovies.Movies, movie =>
                        {
                            try
                            {
                                var m = _movieProviders.Single(x => x.Name == providerMovies.Name).GetMovie(movie.ID);

                                if (m != null && m.Movie != null)
                                {
                                    movieDetailsFromProviders.Add(m);
                                }                                

                                Console.WriteLine($"Successfully got movie details  from Provider {providerMovies.Name.ToString()}.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Failed to get movie details for { movie.ID } from Provider {providerMovies.Name.ToString()}. {ex.Message}");
                            }
                        });
                    });

                    var distinctMovies = movieDetailsFromProviders.Distinct(new MovieEqualityComparer());

                    return distinctMovies.GroupBy(x => x.Movie.Title)
                                         .Select(g => g.MinBy(x => decimal.Parse(x.Movie.Price)))
                                         .OrderBy(x => x.Movie.Title)
                                         .ToList();                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to get cheapest deal movies. {ex.Message}");

                    return new List<ProviderMovie>();
                }                
            });
        }        
    }
}
