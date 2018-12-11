using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Models
{
    public class CityDbContext: DbContext
    {
        public CityDbContext(DbContextOptions<CityDbContext> options):base(options)
        {
           Database.EnsureCreatedAsync();
        }

        public DbSet<CityDto> Cities { get; set; }

        public DbSet<PointsofInterestDto> PointsofInterests { get; set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           
        }
    }
}
