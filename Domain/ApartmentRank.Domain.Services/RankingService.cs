using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Interfaces.DomainInterfaces;
using ApartmentRank.Domain.ValueObjects;

namespace ApartmentRank.Domain.Services
{
    public class RankingService : IRankingService
    {
        public RankingService() { }

        public IDictionary<Apartment, int> OrderByPreferences(IEnumerable<Apartment> apartments, IEnumerable<Preference> preferences)
        {
            var ranking = new Dictionary<Apartment, int>();

            foreach (var apartment in apartments)
            {
                var score = 0;
                foreach (var preference in preferences)
                {
                    var apartmentAttribute = apartment.apartmentAttributes.FirstOrDefault(aa => aa.name.Equals(preference.apartmentAttribute.name));

                    if (apartmentAttribute?.added == preference.apartmentAttribute.added)
                    {
                        score += preference.score;
                    }
                }

                apartment.score = score;
                ranking.Add(apartment, score);
            }

            return ranking.OrderBy(r => r.Value).ToDictionary(r => r.Key, r => r.Value);
        }
    }
}
