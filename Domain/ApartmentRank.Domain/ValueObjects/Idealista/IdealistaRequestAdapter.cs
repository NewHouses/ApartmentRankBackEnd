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
            return new IdealistaRequest();
        }

        public string ToJson()
        {
            var idealistaRequest = Convert();
            return idealistaRequest.ToJson();
        }
    }
}
