using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCore.Security.Jwt.Microservice.Entities;

namespace AspNetCore.Security.Jwt.Microservice.Repository
{
    /// <summary>
    /// Interface IMovieRepository
    /// </summary>
    public interface IMovieRepository
    {
        Task<IEnumerable<ProviderMovie>> GetCheapestDeal(string title);
    }
}
