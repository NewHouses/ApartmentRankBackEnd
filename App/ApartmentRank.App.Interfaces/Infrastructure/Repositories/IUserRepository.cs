using ApartmentRank.App.Interfaces.DTOs;

namespace ApartmentRank.App.Interfaces.Infrastructure.Repositories
{
    public interface IUserRepository : IDisposable
    {
        Task Add(UserDto newUser);
        void Save();
    }
}
