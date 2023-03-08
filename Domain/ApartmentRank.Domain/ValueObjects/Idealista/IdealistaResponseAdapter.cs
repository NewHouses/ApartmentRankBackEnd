using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Entities.Idealista;
using ApartmentRank.Domain.Interfaces;

namespace ApartmentRank.Domain.ValueObjects.Idealista
{
    public class IdealistaResponseAdapter : IResponseAdapter
    {
        public readonly IdealistaResponse idealistaResponse;

        public IdealistaResponseAdapter(IdealistaResponse idealistaResponse)
        {
            this.idealistaResponse = idealistaResponse;
        }

        public ApartmentRankResponse Convert()
        {
            var apartments = idealistaResponse.elementList.Select(ap =>
            {
                var name = ap.suggestedTexts?.title + " " + ap.suggestedTexts?.subtitle;
                var parkingSpace = ap.parkingSpace is null ? new ParkingSpace() : ap.parkingSpace;
                var apartment = new Apartment(name, ap.description, ap.thumbnail, ap.price, ap.propertyType, ap.operation, ap.size, ap.exterior, ap.rooms, ap.bathrooms, ap.latitude, ap.longitude, ap.url, ap.status, ap.newDevelopment, parkingSpace);
                apartment.apartmentAttributes = new [] { new ApartmentAttribute("hasLift", ap.hasLift) };
                return apartment;
            }
            );
           
            return new ApartmentRankResponse(apartments.ToArray(), idealistaResponse.total);
        }

        public string ToJson()
        {
            throw new NotImplementedException();
        }
    }
}
