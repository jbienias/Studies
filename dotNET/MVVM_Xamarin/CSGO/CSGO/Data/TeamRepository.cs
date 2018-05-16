using CSGO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGO.Data
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DatabaseContext _databaseContext;

        public TeamRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            try
            {
                var teams = await _databaseContext.Teams.Include(p => p.Players).ToListAsync();
                return teams;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            try
            {
                var team = await _databaseContext.Teams.Include(p => p.Players).SingleOrDefaultAsync(x => x.Id == id);
                return team;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> AddTeamAsync(Team team)
        {
            try
            {
                var tracking = await _databaseContext.Teams.AddAsync(team);
                var isAdded = tracking.State == EntityState.Added;

                await _databaseContext.SaveChangesAsync();
                return isAdded;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateTeamAsync(Team team)
        {
            try
            {
                var tracking = _databaseContext.Teams.Update(team);
                var isUpdated = tracking.State == EntityState.Modified;

                await _databaseContext.SaveChangesAsync();
                return isUpdated;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteTeamAsync(int id)
        {
            try
            {
                var team = await _databaseContext.Teams.FindAsync(id);
                var tracking = _databaseContext.Teams.Remove(team);
                var isDeleted = tracking.State == EntityState.Deleted;

                await _databaseContext.SaveChangesAsync();
                return isDeleted;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Team>> QueryTeams(Func<Team, bool> predicate)
        {
            try
            {
                var teams = _databaseContext.Teams.Where(predicate);
                return teams.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
