namespace ApartmentRank.Domain.ValueObjects
{
    public class PreferenceArea
    {
        public Area area { get; set; }
        public int score { get; set; }

        public PreferenceArea(Area area, int score)
        {
            this.area = area;
            this.score = score;
        }
    }
}