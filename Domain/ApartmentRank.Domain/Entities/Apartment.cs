using ApartmentRank.Domain.ValueObjects;

namespace ApartmentRank.Domain.Entities
{
    public class Apartment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<ApartmentAttribute> ApartmentAttributes { get; set; }

        public Apartment(Guid id, string name, string description, IEnumerable<ApartmentAttribute> apartmentAttributes)
        {
            Id = id;
            Name = name;
            Description = description;
            ApartmentAttributes = apartmentAttributes;
        }
    }
}