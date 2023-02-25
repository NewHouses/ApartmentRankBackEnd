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
                    var apartmentAttribute = apartment.ApartmentAttributes.FirstOrDefault(aa => aa.Name.Equals(preference.ApartmentAttribute.Name));

                    if (apartmentAttribute?.added == preference.ApartmentAttribute.added)
                    {
                        score += preference.Score;
                    }
                }

                ranking.Add(apartment, score);
            }

            return ranking.OrderBy(r => r.Value).ToDictionary(r => r.Key, r => r.Value);
        }
    }
}
