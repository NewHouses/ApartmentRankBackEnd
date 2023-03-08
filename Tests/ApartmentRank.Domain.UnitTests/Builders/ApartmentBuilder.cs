using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.ValueObjects;

namespace ApartmentRank.Domain.UnitTests.Builders
{
    public class ApartmentBuilder 
    {
        private string name = "Apartament";
        private string description = "Normal";
        private double price = 643;
        private double size = 60;
        private int rooms = 2;
        private int bathrooms = 1;
        private double latitude = 42.2260221;
        private double longitude = -8.7274874;
        private ParkingSpace parkingSpace = new ParkingSpace();
        private IEnumerable<ApartmentAttribute> apartmentAttributes = new List<ApartmentAttribute>();
        public ApartmentBuilder() 
        {
        }

        public Apartment Build()
        {
            return new Apartment()
            {
                name = name,
                description  = description ,
                price = price,
                size = size,
                rooms = rooms,
                bathrooms = bathrooms,
                latitude = latitude,
                longitude = longitude,
                parkingSpace = parkingSpace,
                apartmentAttributes = apartmentAttributes
            };
        }

        public ApartmentBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public ApartmentBuilder WithDescription(string description)
        {
            this.description= description;
            return this;
        }

        public ApartmentBuilder WithPrice(double price)
        {
            this.price= price;
            return this;
        }

        public ApartmentBuilder WithSize(double size)
        {
            this.size = size;
            return this;
        }

        public ApartmentBuilder WithRooms(int rooms)
        {
            this.rooms = rooms;
            return this;
        }

        public ApartmentBuilder WithBathrooms(int bathrooms)
        {
            this.bathrooms = bathrooms;
            return this;
        }

        public ApartmentBuilder WithLatitudeAndLongitude(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
            return this;
        }

        public ApartmentBuilder WithParkingSpace(ParkingSpace parkingSpace)
        {
            this.parkingSpace= parkingSpace;
            return this;
        }

        public ApartmentBuilder WithApartmentAttributes(IEnumerable<ApartmentAttribute> apartmentAttributes)
        {
            this.apartmentAttributes = apartmentAttributes;
            return this;
        }

        public static Apartment BuildBadApartment() 
        {
            return new ApartmentBuilder()
                    .WithName("BadApartment")
                    .WithDescription("Ugly")
                    .WithPrice(900)
                    .WithSize(40)
                    .WithRooms(1)
                    .WithBathrooms(1)
                    .WithLatitudeAndLongitude(42.2246714, - 8.7116302)
                    .WithApartmentAttributes(new ApartmentAttribute[] {
                    new ApartmentAttribute("hasWashMachine", false),
                    new ApartmentAttribute("allowPets", false)
                    })
                    .Build();
        }

        public static Apartment BuildGoodApartment()
        {
            return new ApartmentBuilder()
                    .WithName("GoodApartment")
                    .WithDescription("Nice")
                    .WithApartmentAttributes(new ApartmentAttribute[] {
                    new ApartmentAttribute("hasWashMachine", true),
                    new ApartmentAttribute("allowPets", false)
                    })
                    .Build();
        }
        public static Apartment BuildPerfectApartment()
        {
            return new ApartmentBuilder()
                    .WithName("PerfectApartment")
                    .WithDescription("Incredible")
                    .WithPrice(500)
                    .WithSize(80)
                    .WithRooms(2)
                    .WithBathrooms(2)
                    .WithLatitudeAndLongitude(42.2187919, -8.749663)
                    .WithParkingSpace(new ParkingSpace() 
                    { 
                        hasParkingSpace = true,
                        isParkingSpaceIncludedInPrice= true
                    })
                    .WithApartmentAttributes(new ApartmentAttribute[] {
                    new ApartmentAttribute("hasWashMachine", true),
                    new ApartmentAttribute("allowPets", true)
                    })
                    .Build();
        }
    }
}
