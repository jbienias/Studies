using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSGO.Data;
using CSGO.Models;
using Microsoft.AspNetCore.Authorization;

namespace CSGO.Controllers {
    public class PlayersController_OLD : Controller {
        private readonly IDbContext _context;

        public PlayersController_OLD(IDbContext context) {
            _context = context;
        }

        public PlayersController_OLD()
        {
            _context = new FakeDbContext();
        }

        [Authorize(Roles = "User, Administrator")]
        public async Task<IActionResult> Index(string nickname) {
            var applicationDbContext = from m in _context.Players.Include(p => p.Team)
                                       select m;
            if (!String.IsNullOrEmpty(nickname)) {
                applicationDbContext = applicationDbContext.Where(s => s.Nickname.Contains(nickname));
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Players/Details/5
        [Authorize(Roles = "User, Administrator")]
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.Team)
                .SingleOrDefaultAsync(m => m.PlayerId == id);
            if (player == null) {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create() {
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name");
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("PlayerId,Name,Surname,Nickname,Salary,DateOfBirth,TeamId")] Player player) {
            if (ModelState.IsValid) {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name", player.TeamId);
            return View(player);
        }

        // GET: Players/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var player = await _context.Players.SingleOrDefaultAsync(m => m.PlayerId == id);
            if (player == null) {
                return NotFound();
            }
            ViewData["TeamId"] = new SelectList(_context.Players, "TeamId", "Name", player.TeamId);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerId,Name,Surname,Nickname,Salary,DateOfBirth,TeamId")] Player player) {
            if (id != player.PlayerId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!PlayerExists(player.PlayerId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Players, "TeamId", "Name", player.TeamId);
            return View(player);
        }

        // GET: Players/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.Team)
                .SingleOrDefaultAsync(m => m.PlayerId == id);
            if (player == null) {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var player = await _context.Players.SingleOrDefaultAsync(m => m.PlayerId == id);
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public bool PlayerExists(int id) {
            return _context.Players.Any(e => e.PlayerId == id);
        }

        public bool PlayerExists(Player p) {
            return _context.Players.Any(e => e.Name == p.Name && e.Surname == p.Surname);
        }
    }
}
