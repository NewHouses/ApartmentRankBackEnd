using ApartmentRank.Domain.Interfaces;
using System.Text.Json;

namespace ApartmentRank.Domain.Entities.Idealista
{
    public class IdealistaRequest : IRequest
    {
        public string operation { get; set; }
        public string propertyType { get; set; }
        public string center { get; set; }
        public int distance { get; set; }
        public int maxPrice { get; set; }
        public bool studio { get; set; }
        public int bedrooms { get; set; }
        public string furnished { get; set; }

        public IdealistaRequest(string operation, string propertyType, string center, int distance, int maxPrice, bool studio, int bedrooms, string furnished)
        {
            this.operation = operation;
            this.propertyType = propertyType;
            this.center = center;
            this.distance = distance;
            this.maxPrice = maxPrice;
            this.studio = studio;
            this.bedrooms = bedrooms;
            this .furnished = furnished;
        }
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
