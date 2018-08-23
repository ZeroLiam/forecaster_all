using Microsoft.EntityFrameworkCore;

namespace forecaster_backend.Models
{
    public class ForecastContext : DbContext
    {
        public ForecastContext(DbContextOptions<ForecastContext> options)
            : base(options)
        {
        }

        public DbSet<ForecastItem> ForecastItems { get; set; }
    }
}