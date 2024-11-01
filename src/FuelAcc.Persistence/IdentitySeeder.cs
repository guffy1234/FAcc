using FuelAcc.Application.Interface;
using FuelAcc.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FuelAcc.Persistence.Repositories
{
    public class IdentitySeeder
    {
        private const string AdminRoleName = "admin";
        private const string AdminUserName = "admin";
        private const string AdminPassword = "Admin!1";
        private const string AdminEmail = "admin@gmail.com";
        private const string EmployeeRoleName = "employee";

        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {

            if (await roleManager.FindByNameAsync(AdminRoleName) == null)
            {
                var adminRole = new ApplicationRole()
                {
                    Name = AdminRoleName
                };
                await roleManager.CreateAsync(adminRole);
                await roleManager.AddClaimAsync(adminRole, new Claim(AdminRoleName, true.ToString()));
            }
            if (await roleManager.FindByNameAsync(EmployeeRoleName) == null)
            {
                var employeeRole = new ApplicationRole()
                {
                    Name = EmployeeRoleName
                };
                var result = await roleManager.CreateAsync(employeeRole);
                if (result.Succeeded)
                {
                    employeeRole = await roleManager.FindByNameAsync(EmployeeRoleName);
                    var claims = ClaimsHelper.MakeAllClaims();
                    foreach (var claim in claims)
                    {
                        await roleManager.AddClaimAsync(employeeRole, claim);
                    }
                }
            }
            if (await userManager.FindByNameAsync(AdminUserName) == null)
            {
                var admin = new ApplicationUser { Email = AdminEmail, UserName = AdminUserName };
                var result = await userManager.CreateAsync(admin, AdminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, AdminRoleName);
                    var claims = ClaimsHelper.MakeAllClaims();
                    await userManager.AddClaimsAsync(admin, claims);
                }
            }
        }
    }
}