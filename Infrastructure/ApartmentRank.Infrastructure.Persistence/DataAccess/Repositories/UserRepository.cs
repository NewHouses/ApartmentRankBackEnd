using ApartmentRank.App.Interfaces.DTOs;
using ApartmentRank.App.Interfaces.Infrastructure.Repositories;
using ApartmentRank.Infrastructure.Persistence.Model;

namespace ApartmentRank.Infrastructure.Persistence.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbApartmentRankContext dbContext;

        public UserRepository(DbApartmentRankContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Add(UserDto newUserDto)
        {
            var newUser = DtoToEntity(newUserDto);
            await dbContext.Users.AddAsync(newUser);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            dbContext.SaveChanges();
            Dispose();
        }

        private static UserEntity DtoToEntity(UserDto userDto)
        {
            return new UserEntity()
            {
                UserId = userDto.UserId,
                Username = userDto.Username,
                Password = userDto.Password
            };
        }
    }
}
