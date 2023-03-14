using ApartmentRank.Domain.ValueObjects;
using System.Text.Json;

namespace ApartmentRank.Domain.Entities
{
    public class ApartmentRankRequest
    {
        public Filter filter { get; set; }

        public PreferenceTemplate preferenceTemplate { get; set; }

        public static ApartmentRankRequest FromJson(string request)
        {
            return JsonSerializer.Deserialize<ApartmentRankRequest>(request);
        }
    }
}
