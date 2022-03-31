using Microsoft.EntityFrameworkCore;

namespace BowlerContacts.Models
{
    public class TeamsDbContext : DbContext
    {
        public TeamsDbContext(DbContextOptions<TeamsDbContext> options) : base(options)
        {

        }

        public DbSet<Team> Teams { get; set; }
    }
}
