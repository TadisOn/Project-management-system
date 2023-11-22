using Microsoft.AspNetCore.Identity;

namespace PMS.Auth.Model
{
    public class AuthDbSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<PMSRestUser> _userManager;

        public AuthDbSeeder(UserManager<PMSRestUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            await AddDefaultRoles();
            await AddAdminUser();
        }

        private async Task AddDefaultRoles()
        {
            foreach(var role in PMSRoles.All)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task AddAdminUser()
        {
            var newAdmin = new PMSRestUser
            {
                UserName = "admin",
                Email = "admin@admin.com"
            };

            var existingAdmin = await _userManager.FindByNameAsync(newAdmin.UserName);
            if(existingAdmin == null)
            {
                var createAdminUserResult = await _userManager.CreateAsync(newAdmin, "Verystrong!11");
                if(createAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newAdmin, PMSRoles.All);
                }
            }
        }

    }
}
