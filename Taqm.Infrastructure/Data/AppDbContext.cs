using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taqm.Data.Entities;
using Taqm.Data.Entities.Identity;

namespace Taqm.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
        IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #region Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        #endregion
    }
}
