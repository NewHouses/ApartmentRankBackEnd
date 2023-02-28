namespace ApartmentRank.Domain.ValueObjects
{
    public class ApartmentAttribute
    {
        public string name { get; set; }
        public bool added { get; set; }

        public ApartmentAttribute(string name, bool added)
        {
            this.name = name;
            this.added = added;
        }
    }
}