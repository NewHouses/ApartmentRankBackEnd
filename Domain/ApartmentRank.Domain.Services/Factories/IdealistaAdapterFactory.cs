using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Entities.Idealista;
using ApartmentRank.Domain.Interfaces;
using ApartmentRank.Domain.Services.Interfaces;
using ApartmentRank.Domain.ValueObjects.Idealista;

namespace ApartmentRank.Domain.Services.Factories
{
    public class IdealistaAdapterFactory : IAdapterFactory
    {
        public IRequestAdapter CreateRequestAdapter(ApartmentRankRequest apartmentRankRequest)
        {
            return new IdealistaRequestAdapter(apartmentRankRequest);
        }

        public IResponseAdapter CreateResponseAdapter(string[] apiResponses)
        {
            var idealistaResponses = new List<IdealistaResponse>();
            foreach(var apiResponse in apiResponses)
            {
                var idealistaResponse = IdealistaResponse.FromJson(apiResponse);
                idealistaResponses.Add(idealistaResponse);
            }
            return new IdealistaResponseAdapter(idealistaResponses);
        }
    }
}
