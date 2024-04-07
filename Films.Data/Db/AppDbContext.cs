using Films.Data.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace Films.Data.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Category> Categories { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
            Database.EnsureCreated();
        }

    }
}
