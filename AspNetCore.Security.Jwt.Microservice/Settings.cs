using System.Linq;

namespace AspNetCore.Security.Jwt.Microservice
{
    public class Settings
    {
        public string Token { get; set; }
        public int NoOfRetries { get; set; }
        public int CacheDurationInHours { get; set; }
        public string BaseProviderUrl { get; set; }
        public Providers Providers { get; set; }
    }

    public class Providers
    {
        public Provider[] ProviderDetails { get; set; }

        public Provider this[Entities.Provider provider]
        {
            get
            {
                return this.ProviderDetails.Single(p => p.Name == provider.ToString());
            }            
        }
    }

    public class Provider
    {        
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
