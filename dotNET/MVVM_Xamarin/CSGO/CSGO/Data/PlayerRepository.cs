using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSGO.Models;
using Microsoft.EntityFrameworkCore;

namespace CSGO.Data
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly DatabaseContext _databaseContext;

        public PlayerRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<Player>> GetPlayersAsync()
        {
            try
            {
                var players = await _databaseContext.Players.Include(t => t.Team).ToListAsync();
                return players;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            try
            {
                var player = await _databaseContext.Players.Include(t => t.Team).SingleOrDefaultAsync(x => x.Id == id);
                return player;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> AddPlayerAsync(Player player)
        {
            try
            {
                var tracking = await _databaseContext.Players.AddAsync(player);
                var isAdded = tracking.State == EntityState.Added;

                await _databaseContext.SaveChangesAsync();
                return isAdded;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdatePlayerAsync(Player player)
        {
            try
            {
                var tracking = _databaseContext.Players.Update(player);
                var isUpdated = tracking.State == EntityState.Modified;

                await _databaseContext.SaveChangesAsync();
                return isUpdated;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeletePlayerAsync(int id)
        {
            try
            {
                var player = await _databaseContext.Players.FindAsync(id);

                var tracking = _databaseContext.Players.Remove(player);
                var isDeleted = tracking.State == EntityState.Deleted;

                await _databaseContext.SaveChangesAsync();
                return isDeleted;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Player>> QueryPlayersAsync(Func<Player, bool> predicate)
        {
            try
            {
                var players = _databaseContext.Players.Where(predicate);
                return players.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
