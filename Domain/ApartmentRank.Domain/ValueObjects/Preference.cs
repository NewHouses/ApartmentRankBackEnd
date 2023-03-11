namespace ApartmentRank.Domain.ValueObjects
{
    public class Preference
    {
        public string name { get; set; }
        public int score { get; set; }

        public Preference(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }
}