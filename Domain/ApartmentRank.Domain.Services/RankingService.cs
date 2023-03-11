using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Services.Interfaces;
using ApartmentRank.Domain.ValueObjects;

namespace ApartmentRank.Domain.Services
{
    public class RankingService : IRankingService
    {
        public RankingService() { }

        public IDictionary<Apartment, int> OrderByPreferences(IEnumerable<Apartment> apartments, IEnumerable<Preference> preferences)
        {
            ScoreApartments(apartments.ToArray(), preferences);
            return apartments.OrderBy(a => a.score).ToDictionary(a => a, a => a.score);
        }

        public ApartmentRankResponse GetScoredApartmentRankResponse(ApartmentRankResponse apartmentRankResponse, IEnumerable<Preference> preferences)
        {
            ScoreApartments(apartmentRankResponse.apartments.ToArray(), preferences);
            return apartmentRankResponse;
        }

        private void ScoreApartments(Apartment[] apartments, IEnumerable<Preference> preferences)
        {
            foreach (var apartment in apartments)
            {             
                apartment.score = CalculateApartmentScore(apartment, preferences);
            }
        }

        private int CalculateApartmentScore(Apartment apartment, IEnumerable<Preference> preferences)
        {
            var score = PriceScore(apartment.price) 
                + SizeScore(apartment.size) 
                + LocationScore(apartment.longitude, apartment.latitude)
                + (apartment.bathrooms > 1 ? 1 : 0)
                + (apartment.rooms > 1 ? 2 : 0)
                + (apartment.parkingSpace.hasParkingSpace && apartment.parkingSpace.isParkingSpaceIncludedInPrice ? 1 : 0);

            foreach (var preference in preferences)
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
            switch (price)
            {
                case < 600.0:
                    return 6;
                case < 650.0:
                    return 5;
                case < 700.0:
                    return 4;
                case < 750.0:
                    return 3;
                case < 800.0:
                    return 2;
                case < 850.0:
                    return 1;
                default: break;
            };
            return 0;
        }

        public static int SizeScore(double size)
        {
            switch (size)
            {
                case > 89.0:
                    return 3;
                case > 69.0:
                    return 2;
                case > 59.0:
                    return 1;
                default: break;
            };
            return 0;
        }

        public static int LocationScore(double lon, double lat)
        {

            if (IsPointInMinhocaArea(lon, lat))
                return 5;

            if (IsPointInCameliasCastroArea(lon, lat))
                return 4;

            if (IsPointInCentroArea(lon, lat))
                return 3;

            if (IsPointInCoiaBeiramarArea(lon, lat))
                return 2;

            if (IsPointInGranviaArea(lon, lat))
                return 2;

            if (IsPointInBalaidosCastrelosArea(lon, lat))
                return 2;

            if (IsPointInCasablancaArea(lon, lat))
                return 1;

            return 0;
        }

        private static bool IsPointInMinhocaArea(double longitude, double latitude)
        {
            List<(double, double)> vertices = new List<(double, double)>()
            {
                (-8.749664, 42.2187918),
                (-8.7515093, 42.2131979),
                (-8.7457158, 42.2108457),
                (-8.7325408, 42.2197771),
                (-8.7337424, 42.2215886),
                (-8.749664, 42.2187918)
            };

            int i, j;
            bool c = false;
            int nvert = vertices.Count;
            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((vertices[i].Item2 > latitude) != (vertices[j].Item2 > latitude)) &&
                     (longitude < (vertices[j].Item1 - vertices[i].Item1) * (latitude - vertices[i].Item2) / (vertices[j].Item2 - vertices[i].Item2) + vertices[i].Item1))
                    c = !c;
            }
            return c;
        }

        private static bool IsPointInCameliasCastroArea(double longitude, double latitude)
        {
            List<(double, double)> vertices = new List<(double, double)>()
            {
                (-8.7337424, 42.2215886),
                (-8.7325408, 42.2197771),
                (-8.7268438, 42.2218587),
                (-8.7279166, 42.2236067),
                (-8.7274875, 42.2260219),
                (-8.7228526, 42.2297081),
                (-8.7259854, 42.2351417),
                (-8.7301482, 42.234824),
                (-8.7331952, 42.229009),
                (-8.7360062, 42.2243535),
                (-8.7337424, 42.2215886)
            };

            int i, j;
            bool c = false;
            int nvert = vertices.Count;
            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((vertices[i].Item2 > latitude) != (vertices[j].Item2 > latitude)) &&
                     (longitude < (vertices[j].Item1 - vertices[i].Item1) * (latitude - vertices[i].Item2) / (vertices[j].Item2 - vertices[i].Item2) + vertices[i].Item1))
                    c = !c;
            }
            return c;
        }

        private static bool IsPointInCoiaBeiramarArea(double longitude, double latitude)
        {
            List<(double, double)> vertices = new List<(double, double)>()
            {
                (-8.7291397, 42.2395741),
                (-8.7504687, 42.2265462),
                (-8.749664, 42.2187918),
                (-8.7337424, 42.2215886),
                (-8.7360062, 42.2243535),
                (-8.7301482, 42.234824),
                (-8.7291397, 42.2395741)
            };

            int i, j;
            bool c = false;
            int nvert = vertices.Count;
            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((vertices[i].Item2 > latitude) != (vertices[j].Item2 > latitude)) &&
                     (longitude < (vertices[j].Item1 - vertices[i].Item1) * (latitude - vertices[i].Item2) / (vertices[j].Item2 - vertices[i].Item2) + vertices[i].Item1))
                    c = !c;
            }
            return c;
        }

        private static bool IsPointInCentroArea(double longitude, double latitude)
        {
            List<(double, double)> vertices = new List<(double, double)>()
            {
                (-8.7301482, 42.234824),
                (-8.7259854, 42.2351417),
                (-8.721415, 42.2349034),
                (-8.7181105, 42.2408767),
                (-8.7248911, 42.2414804),
                (-8.7291397, 42.2395741),
                (-8.7301482, 42.234824)
            };

            int i, j;
            bool c = false;
            int nvert = vertices.Count;
            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((vertices[i].Item2 > latitude) != (vertices[j].Item2 > latitude)) &&
                     (longitude < (vertices[j].Item1 - vertices[i].Item1) * (latitude - vertices[i].Item2) / (vertices[j].Item2 - vertices[i].Item2) + vertices[i].Item1))
                    c = !c;
            }
            return c;
        }

        private static bool IsPointInGranviaArea(double longitude, double latitude)
        {
            List<(double, double)> vertices = new List<(double, double)>()
            {
                (-8.7325408, 42.2197771),
                (-8.7299551, 42.2168213),
                (-8.7203421, 42.2213026),
                (-8.7128319, 42.2412262),
                (-8.7181105, 42.2408767),
                (-8.721415, 42.2349034),
                (-8.7259854, 42.2351417),
                (-8.7228526, 42.2297081),
                (-8.7274875, 42.2260219),
                (-8.7279166, 42.2236067),
                (-8.7268438, 42.2218587),
                (-8.7325408, 42.2197771)
            };

            int i, j;
            bool c = false;
            int nvert = vertices.Count;
            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((vertices[i].Item2 > latitude) != (vertices[j].Item2 > latitude)) &&
                     (longitude < (vertices[j].Item1 - vertices[i].Item1) * (latitude - vertices[i].Item2) / (vertices[j].Item2 - vertices[i].Item2) + vertices[i].Item1))
                    c = !c;
            }
            return c;
        }

        private static bool IsPointInBalaidosCastrelosArea(double longitude, double latitude)
        {
            List<(double, double)> vertices = new List<(double, double)>()
            {
                (-8.7457158, 42.2108457),
                (-8.735105, 42.2056961),
                (-8.7308993, 42.2069041),
                (-8.7299551, 42.2168213),
                (-8.7325408, 42.2197771),
                (-8.7457158, 42.2108457)
            };

            int i, j;
            bool c = false;
            int nvert = vertices.Count;
            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((vertices[i].Item2 > latitude) != (vertices[j].Item2 > latitude)) &&
                     (longitude < (vertices[j].Item1 - vertices[i].Item1) * (latitude - vertices[i].Item2) / (vertices[j].Item2 - vertices[i].Item2) + vertices[i].Item1))
                    c = !c;
            }
            return c;
        }

        private static bool IsPointInCasablancaArea(double longitude, double latitude)
        {
            List<(double, double)> vertices = new List<(double, double)>()
            {
                (-8.7128319, 42.2412262),
                (-8.7203421, 42.2213026),
                (-8.7116303, 42.2246713),
                (-8.7112869, 42.237636),
                (-8.7128319, 42.2412262)
            };

            int i, j;
            bool c = false;
            int nvert = vertices.Count;
            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((vertices[i].Item2 > latitude) != (vertices[j].Item2 > latitude)) &&
                     (longitude < (vertices[j].Item1 - vertices[i].Item1) * (latitude - vertices[i].Item2) / (vertices[j].Item2 - vertices[i].Item2) + vertices[i].Item1))
                    c = !c;
            }
            return c;
        }
    }
}
