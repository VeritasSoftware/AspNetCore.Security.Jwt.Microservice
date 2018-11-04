using System;

namespace AspNetCore.Security.Jwt.Microservice.Entities
{
    public class UserModel : IAuthenticationUser
    {
        public string Id { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public DateTime DOB { get; set; }
    }
}
