using ApartmentRank.App.Interfaces;
using ApartmentRank.App.Interfaces.Infrastructure;
using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Services;
using ApartmentRank.Domain.Services.Interfaces;
using ApartmentRank.Domain.Services.Factories;
using ApartmentRank.Domain.ValueObjects;

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

        public ApartmentRankResponse GetScoredApartments(string apartmentRankRequest)
        {
            var apartmentRankResponse = GetApiResponse(apartmentRankRequest, idealistaConnector, idealistaApi);
            return rankingService.GetScoredApartmentRankResponse(apartmentRankResponse, Array.Empty<Preference>());
        }

        private ApartmentRankResponse GetApiResponse(string apartmentRankRequest, IConnector connector, IIdealistaApi api)
        {
            var requestJson = connector.TransformRequest(apartmentRankRequest);
            var apiResponse = api.GetApartmentsJson(requestJson);
            return connector.TransformResponse(apiResponse);
        }
    }
}
