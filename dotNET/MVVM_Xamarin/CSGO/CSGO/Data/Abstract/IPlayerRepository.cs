using CSGO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSGO.Data
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetPlayersAsync();
        Task<Player> GetPlayerByIdAsync(int id);
        Task<bool> AddPlayerAsync(Player player);
        Task<bool> UpdatePlayerAsync(Player player);
        Task<bool> DeletePlayerAsync(int id);
        Task<IEnumerable<Player>> QueryPlayersAsync(Func<Player, bool> predicate);
    }
}
