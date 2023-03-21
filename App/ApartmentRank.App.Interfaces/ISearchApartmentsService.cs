using ApartmentRank.Domain.Entities;

namespace ApartmentRank.App.Interfaces
{
    public interface ISearchApartmentsService
    {
        public Task<ApartmentRankResponse> GetScoredApartments(string apartmentRankRequestJson);
    }
}
