using CSGO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSGO.Data
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetTeams();
        Task<IEnumerable<Team>> GetTeams(string teamName);
        Task<Team> GetTeam(int teamId);
        Task<bool> Save();
        bool TeamExists(int teamId);
        void AddTeam(Team team);
        void DeleteTeam(int teamId);
        void UpdateTeam(Team team);
    }
}
