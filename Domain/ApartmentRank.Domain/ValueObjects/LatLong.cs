namespace ApartmentRank.Domain.ValueObjects
{
    public class LatLong
    {
        public double lat { get; set; }
        public double lng { get; set; }

        public LatLong(double lat, double lng)
        {
            this.lat = lat;
            this.lng = lng;
        }
    }
}
