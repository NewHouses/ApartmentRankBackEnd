using ApartmentRank.Domain.Entities;

namespace ApartmentRank.App.Interfaces
{
    public interface ISearchApartmentsService
    {
        public ApartmentRankResponse GetScoredApartments(string apartmentRankRequestJson);
    }
}
