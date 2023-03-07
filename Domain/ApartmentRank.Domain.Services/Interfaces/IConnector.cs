using ApartmentRank.Domain.Entities;

namespace ApartmentRank.Domain.Services.Interfaces
{
    public interface IConnector
    {
        public string TransformRequest(string apartmentRankRequest);

        public ApartmentRankResponse TransformResponse(string apiResponse);
    }
}
