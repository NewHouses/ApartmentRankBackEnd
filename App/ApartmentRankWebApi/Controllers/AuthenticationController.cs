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
        public async Task<IActionResult> RegisterUser(UserDto newUser)
        {
            try
            {
                await repository.Add(newUser);
                repository.Save();
            }
            catch (Exception e)
            {
                return BadRequest(new AuthResponseData() { Message = e.Message });
            }

            return Ok(new AuthResponseData() { Message = "User registered successfully", username = newUser.Username, token = Guid.NewGuid().ToString() });
        }

        public struct AuthResponseData
        {
            public string Message { get; set; }

            public string username { get; set; }

            public string? token { get; set; }
        }
    }
}