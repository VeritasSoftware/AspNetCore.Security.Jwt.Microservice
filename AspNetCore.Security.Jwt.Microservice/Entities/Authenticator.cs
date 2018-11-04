using AspNetCore.Security.Jwt.Microservice.Entities;
using System.Threading.Tasks;

namespace AspNetCore.Security.Jwt.Microservice
{
    public class CustomAuthenticator : IAuthentication<UserModel>
    {
        public async Task<bool> IsValidUser(UserModel user)
        {
            return true;
        }
    }

    public class Authenticator : IAuthentication
    {
        public async Task<bool> IsValidUser(string id, string password)
        {
            return true;
        }
    }
}
