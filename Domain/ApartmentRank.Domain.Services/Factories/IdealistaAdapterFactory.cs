using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Entities.Idealista;
using ApartmentRank.Domain.Interfaces;
using ApartmentRank.Domain.Services.Interfaces;
using ApartmentRank.Domain.ValueObjects.Idealista;

namespace ApartmentRank.Domain.Services.Factories
{
    public class IdealistaAdapterFactory : IAdapterFactory
    {
        public IRequestAdapter CreateRequestAdapter(string apartmentRankRequestJson)
        {
            var apartmentRankRequest = ApartmentRankRequest.FromJson(apartmentRankRequestJson);
            return new IdealistaRequestAdapter(apartmentRankRequest);
        }

        public IResponseAdapter CreateResponseAdapter(string apiResponse)
        {
            var idealistaResponse = IdealistaResponse.FromJson(apiResponse);
            return new IdealistaResponseAdapter(idealistaResponse);
        }
    }
}
