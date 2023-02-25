namespace ApartmentRank.Domain.ValueObjects
{
    public class Preference
    {
        public ApartmentAttribute ApartmentAttribute { get; set; }
        public int Score { get; set; }

        public Preference(ApartmentAttribute apartmentAttribute, int score)
        {
            ApartmentAttribute = apartmentAttribute;
            Score = score;
        }
    }
}