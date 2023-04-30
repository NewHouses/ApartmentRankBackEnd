using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApartmentRank.Infrastructure.Persistence
{
    public class DbApartmentRankContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public string DbPath { get; }

        public DbApartmentRankContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "apartmentRank.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }

    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }

}