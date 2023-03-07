using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Services.Interfaces;
using ApartmentRank.Domain.ValueObjects;
using System.Collections.Generic;

namespace ApartmentRank.Domain.Services
{
    public class RankingService : IRankingService
    {
        public RankingService() { }

        public IDictionary<Apartment, int> OrderByPreferences(IEnumerable<Apartment> apartments, IEnumerable<Preference> preferences)
        {
            ScoreApartments(apartments, preferences);
            return apartments.OrderBy(a => a.score).ToDictionary(a => a, a => a.score);
        }

        public ApartmentRankResponse GetScoredApartmentRankResponse(ApartmentRankResponse apartmentRankResponse, IEnumerable<Preference> preferences)
        {
            ScoreApartments(apartmentRankResponse.apartments, preferences);
            return apartmentRankResponse;
        }

        private void ScoreApartments(IEnumerable<Apartment> apartments, IEnumerable<Preference> preferences)
        {
            foreach (var apartment in apartments)
            {             
                apartment.score = CalculateApartmentScore(apartment, preferences);
            }
        }

        private int CalculateApartmentScore(Apartment apartment, IEnumerable<Preference> preferences)
        {
            var score = 0;

            foreach (var preference in preferences)
            {
                var apartmentAttribute = apartment.apartmentAttributes
                    .FirstOrDefault(aa => aa.name.Equals(preference.apartmentAttribute.name));

                if (apartmentAttribute?.added == preference.apartmentAttribute.added)
                {
                    score += preference.score;
                }
            }

            return score;
        }
    }
}
