using ApartmentRank.App.Interfaces;
using ApartmentRank.App.Interfaces.Infrastructure;
using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Services;
using ApartmentRank.Domain.Services.Interfaces;
using ApartmentRank.Domain.Services.Factories;
using System.Runtime.CompilerServices;

namespace ApartmentRank.App.Services
{
    public class SearchApartmentsService : ISearchApartmentsService
    {
        private IIdealistaApi idealistaApi;
        private IOpenAIApi openAIApi;
        private IConnector idealistaConnector;

        private IRankingService rankingService;
        public SearchApartmentsService(IIdealistaApi idealistaApi, IOpenAIApi openAIApi, IRankingService rankingService)
        { 
            this.idealistaApi = idealistaApi;
            this.openAIApi = openAIApi;
            this.idealistaConnector = new Connector(new IdealistaAdapterFactory());
            this.rankingService = rankingService;
        }

        public async Task<ApartmentRankResponse> GetScoredApartments(string apartmentRankRequestJson)
        {
            var apartmentRankRequest = ApartmentRankRequest.FromJson(apartmentRankRequestJson);
            var apartmentRankResponse = GetApiResponse(apartmentRankRequest, idealistaConnector, idealistaApi);
            var openAITasks = new List<Task>();
            foreach(var apartment in apartmentRankResponse.apartments)
            {
               var task = SetApartmentAttributes(apartment);
               openAITasks.Add(task);
            }
            await Task.WhenAll(openAITasks);
            return rankingService.GetScoredApartmentRankResponse(apartmentRankResponse, apartmentRankRequest.preferenceTemplate);
        }

        private ApartmentRankResponse GetApiResponse(ApartmentRankRequest apartmentRankRequest, IConnector connector, IIdealistaApi api)
        {
            var requestJson = connector.TransformRequest(apartmentRankRequest);
            var apiResponse = api.GetApartmentsJson(requestJson);
            return connector.TransformResponse(apiResponse.ToArray());
        }

        private async Task SetApartmentAttributes(Apartment apartment)
        {
            var task = await openAIApi.GetApartmentAttributes(apartment.description);
            apartment.apartmentAttributes = task;
        }
    }
}
