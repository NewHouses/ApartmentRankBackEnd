using ApartmentRank.Domain.Interfaces;

namespace ApartmentRank.Domain.Services.Interfaces
{
    public interface IAdapterFactory
    {
        public IRequestAdapter CreateRequestAdapter(string apartmentRankRequestJson);

        public IResponseAdapter CreateResponseAdapter(string apiResponse);
    }
}
