using ApartmentRank.App.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentRank.App.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("ApartmentRankFrontendPolicy")]
    public class SearchApartmentsController : ControllerBase
    {
        private readonly ISearchApartmentsService searchApartmentsService;

        public SearchApartmentsController(ISearchApartmentsService searchApartmentsService)
        {
            this.searchApartmentsService = searchApartmentsService;
        }

        [HttpGet(Name = "apartmentRanking")]
        public async Task<string> Get([FromQuery] string apartmentRankRequest)
        {
            var apartments = await searchApartmentsService.GetScoredApartments(apartmentRankRequest);
            return apartments.ToJson();
        }
    }
}