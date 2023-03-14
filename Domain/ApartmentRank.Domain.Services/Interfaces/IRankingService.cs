using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.ValueObjects;

namespace ApartmentRank.Domain.Services.Interfaces
{
    public interface IRankingService
    {
        IDictionary<Apartment, int> OrderByPreferences(IEnumerable<Apartment> apartments, PreferenceTemplate preferenceTemplate);
        public ApartmentRankResponse GetScoredApartmentRankResponse(ApartmentRankResponse apartmentRankResponse, PreferenceTemplate preferenceTemplate);
    }
}
