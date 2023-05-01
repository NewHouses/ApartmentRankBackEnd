using ApartmentRank.App.Interfaces.DTOs;
using ApartmentRank.App.Interfaces.Infrastructure.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentRank.App.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("ApartmentRankFrontendPolicy")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository repository;

        public AuthenticationController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> PostUser(UserDto newUser)
        {
            try
            {
                await repository.Add(newUser);
                repository.Save();
            }
            catch (Exception e)
            {
                return BadRequest(new AuthResponseData() { message = e.Message });
            }

            return Ok(new AuthResponseData() { message = "User registered successfully" });
        }

        public struct AuthResponseData
        {
            public string message { get; set; }
        }
    }
}