using Microsoft.EntityFrameworkCore;

namespace SampleApp.Data
{
    class SakilaDbContextFactory
    {
        public static SakilaDbContext Create(string connectionString) {
            var optionsBuilder = new DbContextOptionsBuilder<SakilaDbContext>();
            optionsBuilder.UseMySql(connectionString);
            var sakilaDbContext = new SakilaDbContext(optionsBuilder.Options);
            return sakilaDbContext;
        }
    }
}