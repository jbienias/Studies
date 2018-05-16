using CSGO.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CSGO.Data
{
    public interface IDbContext
    {
        DbSet<Player> Players { get; set; }
        DbSet<Team> Teams { get; set; }
        EntityEntry Add(object entity);
        EntityEntry Update(object entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}