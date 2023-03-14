using ApartmentRank.App.Interfaces;
using ApartmentRank.App.Interfaces.Infrastructure;
using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Services;
using ApartmentRank.Domain.Services.Interfaces;
using ApartmentRank.Domain.Services.Factories;

namespace ApartmentRank.App.Services
{
    public class SearchApartmentsService : ISearchApartmentsService
    {
        private IIdealistaApi idealistaApi;
        private IConnector idealistaConnector;

        private IRankingService rankingService;
        public SearchApartmentsService(IIdealistaApi idealistaApi, IRankingService rankingService)
        { 
            this.idealistaApi = idealistaApi;
            this.idealistaConnector = new Connector(new IdealistaAdapterFactory());
            this.rankingService = rankingService;
        }

        public ApartmentRankResponse GetScoredApartments(string apartmentRankRequestJson)
        {
            var apartmentRankRequest = ApartmentRankRequest.FromJson(apartmentRankRequestJson);
            var apartmentRankResponse = GetApiResponse(apartmentRankRequest, idealistaConnector, idealistaApi);
            return rankingService.GetScoredApartmentRankResponse(apartmentRankResponse, apartmentRankRequest.preferenceTemplate);
        }

        private ApartmentRankResponse GetApiResponse(ApartmentRankRequest apartmentRankRequest, IConnector connector, IIdealistaApi api)
        {
            var requestJson = connector.TransformRequest(apartmentRankRequest);
            var apiResponse = api.GetApartmentsJson(requestJson);
            return connector.TransformResponse(apiResponse);
        }
    }
}
