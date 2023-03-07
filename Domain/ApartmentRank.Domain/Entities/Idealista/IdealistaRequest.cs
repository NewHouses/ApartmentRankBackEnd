using ApartmentRank.Domain.Interfaces;
using System.Text.Json;

namespace ApartmentRank.Domain.Entities.Idealista
{
    public class IdealistaRequest : IRequest
    {
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
