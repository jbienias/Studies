using CSGO.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGO.Data
{
    public class FakePlayerRepository : IPlayerRepository
    {
        private List<Player> players = new List<Player>();

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            return await Task.Run(() => players);
        }

        public async Task<IEnumerable<Player>> GetPlayers(string nickname)
        {
            return await Task.Run(() => players.Where(x => x.Nickname.Contains(nickname)));
        }

        public async Task<Player> GetPlayer(int id)
        {
            return await Task.Run(() => players.FirstOrDefault(x => x.PlayerId == id));
        }

        public bool PlayerExists(int id)
        {
            return players.Any(e => e.PlayerId == id);
        }

        public bool PlayerExists(Player p)
        {
            return players.Any(e => e.Name == p.Name && e.Surname == p.Surname);
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void DeletePlayer(int playerId)
        {
            Player player = players.FirstOrDefault(x => x.PlayerId == playerId);
            players.Remove(player);
        }

        public void UpdatePlayer(Player player)
        {
            var p = players.FirstOrDefault(x => x.PlayerId == player.PlayerId);
            p = player;
        }

        public async Task<bool> Save()
        {
            return await Task.Run(() => true);
        }
    }
}
