using CSGO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSGO.Data
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetPlayers();
        Task<IEnumerable<Player>> GetPlayers(string nickname);
        Task<Player> GetPlayer(int playerId);
        Task<bool> Save();
        bool PlayerExists(int playerId);
        bool PlayerExists(Player player);
        void AddPlayer(Player player);
        void DeletePlayer(int playerId);
        void UpdatePlayer(Player player);
    }
}
