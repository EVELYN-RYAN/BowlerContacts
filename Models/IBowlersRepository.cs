using System.Linq;
using BowlerContacts.Models;

namespace BowlerContacts
{
    public interface IBowlersRepository
    {
        IQueryable<Bowler> Bowlers { get; }
        //Declare the save/create/delete methods for the EF
        public void SaveBowler(Bowler b);
        public void CreateBowler(Bowler b);
        public void DeleteBowler(Bowler b);
    }
}