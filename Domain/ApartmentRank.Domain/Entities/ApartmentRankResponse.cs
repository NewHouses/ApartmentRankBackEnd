namespace ApartmentRank.Domain.Entities
{
    public class ApartmentRankResponse
    {
        public IEnumerable<Apartment> apartments { get; set; }
        public int total { get; set; }

        public ApartmentRankResponse(IEnumerable<Apartment> apartments, int total) 
        {
            this.apartments = apartments;
            this.total = total;
        }
    }
}
