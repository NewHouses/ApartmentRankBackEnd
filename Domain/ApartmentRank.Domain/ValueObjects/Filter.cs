namespace ApartmentRank.Domain.ValueObjects
{
    public class Filter
    {
        public string operation { get; set; }
        public string propertyType { get; set; }
        public string center { get; set; }
        public int distance { get; set; }
        public int maxPrice { get; set; }
        public bool studio { get; set; }
        public int bedrooms { get; set; }
        public string furnished { get; set; }
    }
}
