namespace ApartmentRank.Domain.ValueObjects
{
    public class Area
    {
        public string name { get; set; }
        public LatLong[] path { get; set; }

        public Area(string name, LatLong[] path)
        {
            this.name = name;
            this.path = path;
        }
    }
}
