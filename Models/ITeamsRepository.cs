using System.Linq;
using BowlerContacts.Models;

namespace BowlerContacts
{
    public interface ITeamsRepository
    {
        IQueryable<Team> Teams { get; }
        //Declare the save/create/delete methods for the EF
        public void SaveTeam(Team t);
        public void CreateTeam(Team t);
        public void DeleteTeam(Team t);
    }
}
