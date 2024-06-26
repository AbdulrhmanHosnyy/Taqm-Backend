﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Taqm.Data.Entities.Identity;

namespace Taqm.Infrastructure.Seeders
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> userManager)
        {
            var usersCount = await userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaultUser = new User()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "Admin",
                    LastName = "One",
                    PhoneNumber = "123456",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };
                await userManager.CreateAsync(defaultUser, "Admin_123");
                await userManager.AddToRoleAsync(defaultUser, "Admin");
            }
        }
    }
}
