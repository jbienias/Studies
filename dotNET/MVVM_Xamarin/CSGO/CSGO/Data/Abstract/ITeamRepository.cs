using CSGO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSGO.Data
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetTeamsAsync();
        Task<Team> GetTeamByIdAsync(int id);
        Task<bool> AddTeamAsync(Team team);
        Task<bool> UpdateTeamAsync(Team team);
        Task<bool> DeleteTeamAsync(int id);
        Task<IEnumerable<Team>> QueryTeams(Func<Team, bool> predicate);
    }
}
