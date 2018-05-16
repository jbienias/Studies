using Microsoft.EntityFrameworkCore;
using CSGO.Models;
using System.Threading.Tasks;
using System;
using System.Threading;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CSGO.Data
{
    public class FakeDbContext : IDbContext
    {
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        public EntityEntry Add(object o)
        {
            throw new NotImplementedException();
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public EntityEntry Update(object o)
        {
            throw new NotImplementedException();
        }
        public Task<int> SaveChangesAsync(CancellationToken c)
        {
            throw new NotImplementedException();
        }
    }
}
