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
        private IBowlersRepository _repoBowler { get; set; }
        private ITeamsRepository _repoTeam { get; set; }
        public HomeController(IBowlersRepository bowl, ITeamsRepository team)
        {
            _repoBowler = bowl;
            _repoTeam = team;
        }
        

        public IActionResult Index()
        {
            ViewBag.Teams = _repoTeam.Teams
                .ToList();
            
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

            ViewBag.Selected = _repoTeam.Teams
                .Single(x => x.TeamID == id);
                

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

            return View("Form");
        }
        [HttpGet]
        public IActionResult Form()
        {
            ViewBag.Teams = _repoTeam.Teams
                .ToList();

            return View();
        }
        [HttpPost]
        public IActionResult Form(Bowler b)
        {
            ViewBag.Teams = _repoTeam.Teams
                .ToList();
            if (ModelState.IsValid)
            {
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
