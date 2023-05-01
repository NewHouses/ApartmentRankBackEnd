using ApartmentRank.Infrastructure.EnvironmentAccess;
using ApartmentRank.Infrastructure.Persistence.Model;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRank.Infrastructure.Persistence
{
    public class DbApartmentRankContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySql(EnvironmentVariables.ConnectionString, ServerVersion.AutoDetect(EnvironmentVariables.ConnectionString));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.Password).IsRequired();
            });
        }
    }
}