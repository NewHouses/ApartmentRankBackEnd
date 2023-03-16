namespace ApartmentRank.App.Interfaces.Infrastructure
{
    public interface IIdealistaApi
    {
        public IEnumerable<string> GetApartmentsJson(string request);
    }
}