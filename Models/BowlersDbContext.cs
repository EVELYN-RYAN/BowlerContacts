using Microsoft.EntityFrameworkCore;

namespace BowlerContacts.Models
{
    public class BowlersDbContext : DbContext
    {
        public BowlersDbContext(DbContextOptions<BowlersDbContext> options) : base(options)
        {

        }
        //Need to declare both to properly build the foreign key relationship
        public DbSet<Bowler> Bowlers { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}