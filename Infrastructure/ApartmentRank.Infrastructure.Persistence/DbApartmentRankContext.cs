using ApartmentRank.Infrastructure.EnvironmentAccess;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRank.Infrastructure.Persistence
{
    public class DbApartmentRankContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySql(EnvironmentVariables.ConnectionString, ServerVersion.AutoDetect(EnvironmentVariables.ConnectionString));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.Password).IsRequired();
            });
        }
    }

    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }

}