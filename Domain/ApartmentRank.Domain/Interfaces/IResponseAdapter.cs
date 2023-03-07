using ApartmentRank.Domain.Entities;

namespace ApartmentRank.Domain.Interfaces
{
    public interface IResponseAdapter
    {
        public ApartmentRankResponse Convert();

        public string ToJson();
    }
}
