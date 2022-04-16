using Logo.Proje.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Logo.Proje.Data
{
    public class ContextSeed
    {
        public static async Task SeedRoleAsync(UserManager<CustomIdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Manager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Resident.ToString()));
        }
        public static async Task SeedAdminAsync(UserManager<CustomIdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new CustomIdentityUser
            {
                UserName = "admin",
                Email = "admin@admin.com",
                Name = "Yavuz Selim",
                Surname = "GÜLER",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Adm1n!");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Resident.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Manager.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());
                }
            }
        }
    }
}
