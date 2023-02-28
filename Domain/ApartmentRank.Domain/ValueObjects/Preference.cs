namespace ApartmentRank.Domain.ValueObjects
{
    public class Preference
    {
        public ApartmentAttribute apartmentAttribute { get; set; }
        public int score { get; set; }

        public Preference(ApartmentAttribute apartmentAttribute, int score)
        {
            this.apartmentAttribute = apartmentAttribute;
            this.score = score;
        }
    }
}