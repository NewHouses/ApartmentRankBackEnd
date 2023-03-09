using ApartmentRank.App.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentRank.App.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchApartmentsController : ControllerBase
    {
        private readonly ISearchApartmentsService searchApartmentsService;

        public SearchApartmentsController(ISearchApartmentsService searchApartmentsService)
        {
            this.searchApartmentsService = searchApartmentsService;
        }

        [HttpGet(Name = "apartmentRanking")]
        public string Get([FromQuery] string apartmentRankRequest)
        {
            return searchApartmentsService.GetScoredApartments(apartmentRankRequest).ToJson();
        }
    }
}