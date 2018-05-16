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
        private readonly ApplicationDbContext _context;

        public TeamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _context.Teams.Include(p => p.Players).ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetTeams(string name)
        {
            return await _context.Teams.Include(p => p.Players).Where(n => n.Name.Contains(name)).ToListAsync();
        }

        public async Task<Team> GetTeam(int id)
        {
            return await _context.Teams.SingleOrDefaultAsync(x => x.TeamId == id);
        }

        public bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.TeamId == id);
        }

        public void AddTeam(Team team)
        {
            _context.Teams.Add(team);
        }

        public void DeleteTeam(int teamId)
        {
            Team team = _context.Teams.Find(teamId);
            _context.Teams.Remove(team);
        }

        public void UpdateTeam(Team team)
        {
            _context.Update(team);
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
