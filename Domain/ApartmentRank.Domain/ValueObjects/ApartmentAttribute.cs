namespace ApartmentRank.Domain.ValueObjects
{
    public class ApartmentAttribute
    {
        public string Name { get; set; }
        public bool added { get; set; }

        public ApartmentAttribute(string name, bool added)
        {
            Name = name;
            this.added = added;
        }
    }
}