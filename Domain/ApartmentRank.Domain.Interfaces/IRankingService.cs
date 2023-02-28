using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.ValueObjects;

namespace ApartmentRank.Domain.Interfaces.DomainInterfaces
{
    public interface IRankingService
    {
        IDictionary<Apartment, int> OrderByPreferences(IEnumerable<Apartment> apartments, IEnumerable<Preference> preferences);
        public ApartmentRankResponse GetScoredApartmentRankResponse(ApartmentRankResponse apartmentRankResponse, IEnumerable<Preference> preferences);
    }
}
