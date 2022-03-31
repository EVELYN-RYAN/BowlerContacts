using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BowlerContacts.Models;
using Microsoft.EntityFrameworkCore;

namespace BowlerContacts.Controllers
{
    public class HomeController : Controller
    {
        // Bring in the repositories
        private IBowlersRepository _repoBowler { get; set; }
        private ITeamsRepository _repoTeam { get; set; }
        //Construct both in HomeController
        public HomeController(IBowlersRepository bowl, ITeamsRepository team)
        {
            _repoBowler = bowl;
            _repoTeam = team;
        }
        

        public IActionResult Index()
        {
            //Since bowlers are passed.. Build Viewbag for teams
            ViewBag.Teams = _repoTeam.Teams
                .ToList();
            //All bowlers in a list ID ASC with Team linked
            var bowlers = _repoBowler.Bowlers
                .Include(x=> x.Team)
                .OrderBy(x => x.BowlerID)
                .ToList();

            return View(bowlers);
        }
        public IActionResult Filtered(int id)
        {
            ViewBag.Teams = _repoTeam.Teams
                .ToList();

            //Take the parameter and deliver the teams that will be. Only necessary for filtering by 1 team
            ViewBag.Selected = _repoTeam.Teams
                .Single(x => x.TeamID == id);
                
            //Only desplay selected contacts that have the same team id
            var bowlers = _repoBowler.Bowlers
                .Include(x => x.Team)
                .Where(x=>x.TeamID == id)
                .ToList();

            return View(bowlers);
        }

        public IActionResult NewBowler()
        {
            ViewBag.Teams = _repoTeam.Teams
                .ToList();
            //Pass nothing but the form
            return View("Form");
        }
        //Only necessary if somethin uses the URL to access the form
        [HttpGet]
        public IActionResult Form()
        {
            ViewBag.Teams = _repoTeam.Teams
                .ToList();

            return View();
        }
        //Take the bowler object passed in post
        [HttpPost]
        public IActionResult Form(Bowler b)
        {
            //Viewbag necessary for invalid entries
            ViewBag.Teams = _repoTeam.Teams
                .ToList();
            if (ModelState.IsValid)
            {
                //Check to see if Bowler is existing or New (New bowlers show BowlerID as 0)
                if (b.BowlerID == 0)
                {
                    _repoBowler.CreateBowler(b);
                    return RedirectToAction("Index");
                }
                else
                {
                    _repoBowler.SaveBowler(b);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(b);
            }
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Teams = _repoTeam.Teams
                .ToList();
            //Pass the single instance of bowler with corresponding ID
            var bowlers = _repoBowler.Bowlers
                .Single(x => x.BowlerID == id);

            return View("Form",bowlers);
        }
        
        public IActionResult Delete(int id)
        {
            var bowlers = _repoBowler.Bowlers
                .Single(x => x.BowlerID == id);
            _repoBowler.DeleteBowler(bowlers);

            return RedirectToAction("Index");
        }
    }
}
