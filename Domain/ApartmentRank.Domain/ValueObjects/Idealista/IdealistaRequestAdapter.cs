using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Entities.Idealista;
using ApartmentRank.Domain.Interfaces;

namespace ApartmentRank.Domain.ValueObjects.Idealista
{
    public class IdealistaRequestAdapter : IRequestAdapter
    {
        public readonly ApartmentRankRequest apartmentRankRequest;

        public IdealistaRequestAdapter(ApartmentRankRequest apartmentRankRequest)
        {
            this.apartmentRankRequest = apartmentRankRequest;
        }

        public IRequest Convert()
        {
            return new IdealistaRequest(
                    apartmentRankRequest.filter.operation,
                    apartmentRankRequest.filter.propertyType,
                    apartmentRankRequest.filter.center,
                    (int)apartmentRankRequest.filter.distance,
                    apartmentRankRequest.filter.maxPrice,
                    apartmentRankRequest.filter.studio,
                    apartmentRankRequest.filter.bedrooms,
                    apartmentRankRequest.filter.furnished
                );
        }

        public string ToJson()
        {
            var idealistaRequest = Convert();
            return idealistaRequest.ToJson();
        }
    }
}
