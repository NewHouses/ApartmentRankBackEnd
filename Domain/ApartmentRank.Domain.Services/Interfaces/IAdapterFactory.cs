using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Interfaces;

namespace ApartmentRank.Domain.Services.Interfaces
{
    public interface IAdapterFactory
    {
        public IRequestAdapter CreateRequestAdapter(ApartmentRankRequest apartmentRankRequest);

        public IResponseAdapter CreateResponseAdapter(string[] apiResponse);
    }
}
