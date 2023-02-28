using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Entities.Idealista;

namespace ApartmentRank.Domain.ValueObjects.Idealista
{
    public class IdealistaResponseAdapter
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
                var apartment = new Apartment(name, ap.description, ap.thumbnail, ap.price, ap.propertyType, ap.operation, ap.size, ap.exterior, ap.rooms, ap.bathrooms, ap.latitude, ap.longitude, ap.url, ap.status, ap.newDevelopment, ap.parkingSpace);
                apartment.apartmentAttributes = new [] { new ApartmentAttribute("hasLift", ap.hasLift) };
                return apartment;
            }
            );
           
            return new ApartmentRankResponse(apartments, idealistaResponse.total);
        }
    }
}
