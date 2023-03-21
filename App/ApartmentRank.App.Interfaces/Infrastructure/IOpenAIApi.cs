using ApartmentRank.Domain.ValueObjects;

namespace ApartmentRank.App.Interfaces.Infrastructure
{
    public interface IOpenAIApi
    {
        Task<IEnumerable<ApartmentAttribute>> GetApartmentAttributes(string apartmentDescription);
    }
}
