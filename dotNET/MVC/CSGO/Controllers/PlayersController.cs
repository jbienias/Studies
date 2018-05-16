using CSGO.Data;
using CSGO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CSGO.Controllers
{
    public class PlayersController : Controller
    {

        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;

        public PlayersController(IPlayerRepository playerRepository, ITeamRepository teamRepository)
        {
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
        }

        [Authorize(Roles = "User, Administrator")]
        public async Task<IActionResult> Index(string nickname)
        {
            if (!String.IsNullOrEmpty(nickname))
                return View(await _playerRepository.GetPlayers(nickname));
            else
                return View(await _playerRepository.GetPlayers());
        }

        // GET: Players/Details/5
        [Authorize(Roles = "User, Administrator")]
        public async Task<IActionResult> Details(int id)
        {
            var player = await _playerRepository.GetPlayer(id);
            if (player == null)
            {
                return View("NotFound");
            }
            return View(player);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create()
        {
            ViewData["TeamId"] = new SelectList(await _teamRepository.GetTeams(), "TeamId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("PlayerId,Name,Surname,Nickname,Salary,DateOfBirth,TeamId")] Player player)
        {
            if (ModelState.IsValid)
            {
                _playerRepository.AddPlayer(player);
                await _playerRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(await _teamRepository.GetTeams(), "TeamId", "Name", player.TeamId);
            return View(player);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
            var player = await _playerRepository.GetPlayer(id);
            if (player == null)
            {
                return View("NotFound");
            }
            ViewData["TeamId"] = new SelectList(await _teamRepository.GetTeams(), "TeamId", "Name", player.TeamId);
            return View(player);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerId,Name,Surname,Nickname,Salary,DateOfBirth,TeamId")] Player player)
        {
            if (id != player.PlayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _playerRepository.UpdatePlayer(player);
                    await _playerRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.PlayerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(await _teamRepository.GetTeams(), "TeamId", "Name", player.TeamId);
            return View(player);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var player = await _playerRepository.GetPlayer(id);
            if (player == null)
            {
                return View("NotFound");
            }
            return View(player);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _playerRepository.GetPlayer(id);
            _playerRepository.DeletePlayer(id);
            await _playerRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        public bool PlayerExists(int id)
        {
            return _playerRepository.PlayerExists(id);
        }

        public bool PlayerExists(Player p)
        {
            return _playerRepository.PlayerExists(p);
        }
    }
}
