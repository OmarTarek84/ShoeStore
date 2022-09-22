using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Data.Seed
{
    public class UserSeed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            if (userManager.Users.Any()) return;

            var user = new AppUser
            {
                UserName = "7amada1234",
                Email = "ahmed@gmail.com",
                FirstName = "Ahmed",
                LastName = "Hussein",
                JoinedAt = DateTime.Now,
            };

            var roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "User" },
                new IdentityRole { Name = "Admin" },
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            await userManager.CreateAsync(user, config.GetSection("SeedUserPassword").Value);
            await userManager.AddToRolesAsync(user, new[] { "User" });

            var admin = new AppUser
            {
                UserName = "Omar84",
                Email = "omar@gmail.com",
                FirstName = "Omar",
                LastName = "Tarek",
                JoinedAt = DateTime.Now,
            };

            try
            {
                await userManager.CreateAsync(admin, config.GetSection("SeedUserPassword").Value);
                await userManager.AddToRolesAsync(admin, new[] { "Admin", "User" });
            }
            catch (Exception ex)
            {

            }
        }
    }
}
