using CSGO.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGO.Data
{
    public class FakeTeamRepository : ITeamRepository
    {
        private List<Team> teams = new List<Team>();

        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await Task.Run(() => teams);
        }

        public async Task<IEnumerable<Team>> GetTeams(string name)
        {
            return await Task.Run(() => teams.Where(x => x.Name.Contains(name)));
        }

        public async Task<Team> GetTeam(int id)
        {
            return await Task.Run(() => teams.FirstOrDefault(x => x.TeamId == id));
        }

        public bool TeamExists(int id)
        {
            return teams.Any(e => e.TeamId == id);
        }

        public void AddTeam(Team t)
        {
            teams.Add(t);
        }

        public void DeleteTeam(int teamId)
        {
            Team t = teams.FirstOrDefault(x => x.TeamId == teamId);
            teams.Remove(t);
        }

        public void UpdateTeam(Team t)
        {
            var p = teams.FirstOrDefault(x => x.TeamId == t.TeamId);
            p = t;
        }

        public async Task<bool> Save()
        {
            return await Task.Run(() => true);
        }
    }
}
