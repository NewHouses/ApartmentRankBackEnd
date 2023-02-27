using System.Text.Json;

namespace ApartmentRank.Domain.Entities.Idealista
{
    public class IdealistaResponse
    {
        public IEnumerable<IdealistaApartment> elementList { get; set; }
        public int total { get; set; }

        public static IdealistaResponse FromJson(string response)
        {
            return JsonSerializer.Deserialize<IdealistaResponse>(response);
        }
    }

    public class IdealistaApartment
    {
        public string thumbnail { get; set; }
        public double price { get; set; }
        public string propertyType { get; set; }
        public string operation { get; set; }
        public double size { get; set; }
        public bool exterior { get; set; }
        public int rooms { get; set; }
        public int bathrooms { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public bool newDevelopment { get; set; }
        public bool hasLift { get; set; }
        public ParkingSpace? parkingSpace { get; set; }
        public SuggestedTexts? suggestedTexts { get; set; }
    }

    public class ParkingSpace
    {
        public bool hasParkingSpace { get; set; }
        public bool isParkingSpaceIncludedInPrice { get; set; }
    }

    public class SuggestedTexts {
        public string subtitle { get; set; }
        public string title { get; set; }
    }
}
