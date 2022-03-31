using System.Linq;

namespace BowlerContacts.Models
{
    public class EFTeamsRepository : ITeamsRepository
    {
        private TeamsDbContext _context { get; set; }
        public EFTeamsRepository(TeamsDbContext temp)
        {
            _context = temp;
        }

        public IQueryable<Team> Teams => _context.Teams;
        //Implement the interface with Save/Add/Delete
        public void SaveTeam(Team t)
        {
            _context.Update(t);
            _context.SaveChanges();
        }

        public void CreateTeam(Team t)
        {
            _context.Add(t);
            _context.SaveChanges();
        }

        public void DeleteTeam(Team t)
        {
            _context.Remove(t);
            _context.SaveChanges();
        }
    }
}
