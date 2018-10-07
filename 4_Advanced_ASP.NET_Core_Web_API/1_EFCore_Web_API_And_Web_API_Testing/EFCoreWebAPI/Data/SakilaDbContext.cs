using Microsoft.EntityFrameworkCore;
using SampleApp.Models;

namespace SampleApp.Data
{
    class SakilaDbContext : DbContext
    {
        public SakilaDbContext(DbContextOptions<SakilaDbContext> options)
        : base(options) { }

        public DbSet<Actor> Actor { get; set; }
        // DbSet<T> type properties for other domain models
    }
}