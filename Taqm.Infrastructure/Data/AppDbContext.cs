using Microsoft.EntityFrameworkCore;
using Taqm.Data.Entities;

namespace Taqm.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #region Tables
        public DbSet<Post> Posts { get; set; }
        #endregion
    }
}
