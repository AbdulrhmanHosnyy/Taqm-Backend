using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Taqm.Data.Entities.Identity;

namespace Taqm.Infrastructure.Seeders
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> roleManager)
        {
            var usersCount = await roleManager.Roles.CountAsync();
            if (usersCount <= 0)
            {
                await roleManager.CreateAsync(new Role()
                {
                    Name = "Admin"
                });
                await roleManager.CreateAsync(new Role()
                {
                    Name = "User"
                });
            }
        }

    }
}
