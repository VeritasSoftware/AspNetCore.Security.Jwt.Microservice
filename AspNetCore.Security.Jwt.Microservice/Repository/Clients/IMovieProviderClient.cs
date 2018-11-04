using System.Threading.Tasks;

namespace AspNetCore.Security.Jwt.Microservice.Repository.Clients
{
    public interface IMovieProviderClient        
    {
        Task<TResponse> Get<TResponse>(string url)
            where TResponse : class;
    }
}
