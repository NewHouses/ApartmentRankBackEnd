using ApartmentRank.Domain.ValueObjects;
using System.Drawing;

namespace ApartmentRank.Domain.Entities
{
    public class Apartment
    {
        public Guid id { get; set; }
        public int score { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public double price { get; set; }
        public string propertyType { get; set; }
        public string operation { get; set; }
        public double size { get; set; }
        public bool isExterior { get; set; }
        public int rooms { get; set; }
        public int bathrooms { get; set; }
        public string address { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string link { get; set; }
        public string status { get; set; }
        public bool newDevelopment { get; set; }
        public ParkingSpace parkingSpace { get; set; }
        public IEnumerable<ApartmentAttribute> apartmentAttributes { get; set; }

        public Apartment()
        {
            this.id = Guid.NewGuid();
        }

        public Apartment(string name, string description, string imageUrl,
            double price, string propertyType, string operation, double size,
            bool isExterior, int rooms, int bathrooms,
            double latitude, double longitude, string link, string status, bool newDevelopment,
            ParkingSpace parkingSpace)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.description = description;
            this.imageUrl = imageUrl;
            this.price = price;
            this.propertyType = propertyType;
            this.operation = operation;
            this.size = size;
            this.isExterior = isExterior;
            this.rooms = rooms;
            this.bathrooms= bathrooms;
            this.latitude = latitude;
            this.longitude = longitude;
            this.link = link;
            this.status = status;
            this.newDevelopment = newDevelopment;
            this.parkingSpace = parkingSpace;
        }
    }
}