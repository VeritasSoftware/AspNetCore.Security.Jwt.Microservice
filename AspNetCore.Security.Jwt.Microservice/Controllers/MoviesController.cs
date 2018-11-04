using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCore.Security.Jwt.Microservice.Entities;
using AspNetCore.Security.Jwt.Microservice.Repository;

namespace AspNetCore.Security.Jwt.Microservice.Controllers
{
    using AspNetCore.Security.Jwt;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        readonly IMovieRepository _moviesRepository;

        public MoviesController(IMovieRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }        

        [HttpGet("cheapest/{title}")]
        public async Task<IEnumerable<ProviderMovie>> GetCheapestDeal(string title)
        {
            var currentUser = HttpContext.User;

            //Verify Claim
            if (currentUser.HasClaim(x => x.Type == "DOB") && currentUser.HasClaim(x => x.Type == IdType.Role.ToClaimTypes()))
            {
                //Do something here
            }

            return await _moviesRepository.GetCheapestDeal(title);
        }        
    }
}
