using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Services.Interfaces;
using ApartmentRank.Domain.ValueObjects;

namespace ApartmentRank.Domain.Services
{
    public class RankingService : IRankingService
    {
        private static double minPrice;
        private static double maxPrice;
        private static int priceWeighing;

        private static double minSize;
        private static double maxSize;
        private static int sizeWeighing;

        public RankingService() { }

        public IDictionary<Apartment, int> OrderByPreferences(IEnumerable<Apartment> apartments, PreferenceTemplate preferenceTemplate)
        {
            SetParameters(apartments.ToArray(), preferenceTemplate);
            ScoreApartments(apartments.ToArray(), preferenceTemplate);
            return apartments.OrderBy(a => a.score).ToDictionary(a => a, a => a.score);
        }

        public ApartmentRankResponse GetScoredApartmentRankResponse(ApartmentRankResponse apartmentRankResponse, PreferenceTemplate preferenceTemplate)
        {
            var apartments = apartmentRankResponse.apartments.ToArray();
            SetParameters(apartments, preferenceTemplate);
            ScoreApartments(apartments, preferenceTemplate);
            return apartmentRankResponse;
        }

        private void ScoreApartments(Apartment[] apartments, PreferenceTemplate preferenceTemplate)
        {
            foreach (var apartment in apartments)
            {             
                apartment.score = CalculateApartmentScore(apartment, preferenceTemplate);
            }
        }

        private static void SetParameters(Apartment[] apartments, PreferenceTemplate preferenceTemplate)
        {
            var pricePreference = preferenceTemplate.preferences.FirstOrDefault(p => p.name.Equals("price"));
            SetPriceParameters(apartments, pricePreference);
            var sizePreference = preferenceTemplate.preferences.FirstOrDefault(p => p.name.Equals("size"));
            SetSizeParameters(apartments, sizePreference);
        }

        private static void SetPriceParameters(Apartment[] apartments, Preference pricePreference)
        {
            minPrice = apartments.Min(apartments => apartments.price);
            maxPrice = apartments.Max(apartments => apartments.price);
            priceWeighing = pricePreference != null ? pricePreference.score + 1 : 6;
        }

        private static void SetSizeParameters(Apartment[] apartments, Preference sizePreference)
        {
            minSize = apartments.Min(apartments => apartments.size);
            maxSize = apartments.Max(apartments => apartments.size);
            sizeWeighing = sizePreference != null ? sizePreference.score + 1 : 6;
        }

        private int CalculateApartmentScore(Apartment apartment, PreferenceTemplate preferenceTemplate)
        {
            var score = PriceScore(apartment.price) 
                + SizeScore(apartment.size) 
                + LocationScore(preferenceTemplate.preferenceAreas.ToArray(), apartment.latitude, apartment.longitude)
                + (apartment.bathrooms > 1 ? 1 : 0)
                + (apartment.rooms > 1 ? 2 : 0)
                + (apartment.parkingSpace.hasParkingSpace && apartment.parkingSpace.isParkingSpaceIncludedInPrice ? 1 : 0);

            foreach (var preference in preferenceTemplate.preferences)
            {
                var apartmentAttribute = apartment.apartmentAttributes
                    .FirstOrDefault(aa => aa.name.Equals(preference.name));

                if (apartmentAttribute is not null && apartmentAttribute.added)
                {
                    score += preference.score;
                }
            }

            return score;
        }

        public static int PriceScore(double price)
        {
            var priceRank = (maxPrice - minPrice) / priceWeighing;
            var score = (int)((maxPrice - price) / priceRank);
            return score - 1;
        }

        public static int SizeScore(double size)
        {
            var sizeRank = (maxSize - minSize) / sizeWeighing;
            var score = (int)((size - minSize) / sizeRank);
            return score - 1;
        }

        public static int LocationScore(PreferenceArea[] preferenceAreas, double lat, double lon)
        {
            foreach(var preferenceArea in preferenceAreas)
            {
                if(IsPointInArea(preferenceArea.area.path, lat, lon))
                    return preferenceArea.score;
            }

            return 0;
        }

        public static bool IsPointInArea(LatLong[] vertices, double Lat, double Lng)
        {
            var j = vertices.Length - 1;
            bool isInside = false;
            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].lng < Lng && vertices[j].lng >= Lng || vertices[j].lng < Lng && vertices[i].lng >= Lng)
                {
                    if (vertices[i].lat + (Lng - vertices[i].lng) / (vertices[j].lng - vertices[i].lng) * (vertices[j].lat - vertices[i].lat) < Lat)
                    {
                        isInside = !isInside;
                    }
                }
                j = i;
            }
            return isInside;
        }
    }
}
