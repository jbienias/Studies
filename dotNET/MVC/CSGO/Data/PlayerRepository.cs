using CSGO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGO.Data
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _context;

        public PlayerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            return await _context.Players.Include(t => t.Team).ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetPlayers(string nickname)
        {
            return await _context.Players.Include(t => t.Team).Where(n => n.Nickname.Contains(nickname)).ToListAsync();
        }

        public async Task<Player> GetPlayer(int id)
        {
            return await _context.Players.SingleOrDefaultAsync(x => x.PlayerId == id);
        }

        public bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.PlayerId == id);
        }

        public bool PlayerExists(Player p)
        {
            return _context.Players.Any(e => e.Name == p.Name && e.Surname == p.Surname);
        }

        public void AddPlayer(Player player)
        {
            _context.Players.Add(player);
        }

        public void DeletePlayer(int playerId)
        {
            Player player = _context.Players.Find(playerId);
            _context.Players.Remove(player);
        }

        public void UpdatePlayer(Player player)
        {
            _context.Update(player);
        }

        public async Task<bool> Save()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { return false; };
        }
    }
}
