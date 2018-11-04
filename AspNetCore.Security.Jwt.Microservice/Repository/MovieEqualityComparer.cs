using System.Collections.Generic;
using AspNetCore.Security.Jwt.Microservice.Entities;

namespace AspNetCore.Security.Jwt.Microservice.Repository
{
    /// <summary>
    /// Class MovieEqualityComparer
    /// </summary>
    public class MovieEqualityComparer : IEqualityComparer<ProviderMovie>
    {
        public bool Equals(ProviderMovie x, ProviderMovie y)
        {
            return x.Provider == y.Provider && x.Movie.Title == y.Movie.Title;
        }

        public int GetHashCode(ProviderMovie obj)
        {
            return obj.Movie.Title.GetHashCode();
        }
    }
}
