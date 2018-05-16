using CSGO.Data;
using CSGO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CSGO.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamRepository _teamRepository;

        public TeamsController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        [Authorize(Roles = "User, Administrator")]
        public async Task<IActionResult> Index(string teamName)
        {
            if (!String.IsNullOrEmpty(teamName))
                return View(await _teamRepository.GetTeams(teamName));
            else
                return View(await _teamRepository.GetTeams());
        }

        [Authorize(Roles = "User, Administrator")]
        public async Task<IActionResult> Details(int id)
        {
            var team = await _teamRepository.GetTeam(id);
            if (team == null)
            {
                return View("NotFound");
            }
            return View(team);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("TeamId,Name,DateOfFounding")] Team team)
        {
            if (ModelState.IsValid)
            {
                _teamRepository.AddTeam(team);
                await _teamRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
            var team = await _teamRepository.GetTeam(id);
            if (team == null)
            {
                return View("NotFound");
            }
            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,Name,DateOfFounding")] Team team)
        {
            if (id != team.TeamId)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _teamRepository.UpdateTeam(team);
                    await _teamRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.TeamId))
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _teamRepository.GetTeam(id);
            if (team == null)
            {
                return View("NotFound");
            }
            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _teamRepository.GetTeam(id);
            _teamRepository.DeleteTeam(id);
            await _teamRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _teamRepository.TeamExists(id);
        }
    }
}
