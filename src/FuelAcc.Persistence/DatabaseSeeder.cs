using FuelAcc.Application.Interface;
using FuelAcc.Application.Interface.Events;
using FuelAcc.Domain.Entities.Dictionaries;
using FuelAcc.Domain.Entities.Other;
using FuelAcc.Domain.Identity;
using FuelAcc.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FuelAcc.Persistence.Repositories
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private const string AdminRoleName = "admin";
        private const string AdminUserName = "admin";
        private const string AdminPassword = "Admin!1";
        private const string AdminEmail = "admin@gmail.com";
        private const string EmployeeRoleName = "employee";

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IEventConverter _eventConverter;

        public DatabaseSeeder(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            AppDbContext context,
            IEventConverter eventConverter)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;
            this._eventConverter = eventConverter;
        }

        public async Task SeedAsync()
        {
            await SeedIdentityAsync();
            await SeedBranchAndSettingsAsync();
        }

        private async Task SeedIdentityAsync()
        {
            if (await _roleManager.FindByNameAsync(AdminRoleName) == null)
            {
                var adminRole = new ApplicationRole()
                {
                    Name = AdminRoleName
                };
                await _roleManager.CreateAsync(adminRole);
                await _roleManager.AddClaimAsync(adminRole, new Claim(AdminRoleName, true.ToString()));
            }
            if (await _roleManager.FindByNameAsync(EmployeeRoleName) == null)
            {
                var employeeRole = new ApplicationRole()
                {
                    Name = EmployeeRoleName
                };
                var result = await _roleManager.CreateAsync(employeeRole);
                if (result.Succeeded)
                {
                    employeeRole = await _roleManager.FindByNameAsync(EmployeeRoleName);
                    var claims = ClaimsHelper.MakeAllClaims();
                    foreach (var claim in claims)
                    {
                        await _roleManager.AddClaimAsync(employeeRole, claim);
                    }
                }
            }
            if (await _userManager.FindByNameAsync(AdminUserName) == null)
            {
                var admin = new ApplicationUser { Email = AdminEmail, UserName = AdminUserName };
                var result = await _userManager.CreateAsync(admin, AdminPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, AdminRoleName);
                    var claims = ClaimsHelper.MakeAllClaims();
                    await _userManager.AddClaimsAsync(admin, claims);
                }
            }
        }

        private const string BranchName = "Main";

        private async Task SeedBranchAndSettingsAsync()
        {
            var admin = await _context.Users.FirstOrDefaultAsync(u => u.UserName == AdminUserName);
            var now = DateTime.UtcNow;

            var main = await _context.Branches.FirstOrDefaultAsync();
            if (main == null)
            {
                main = new Branch()
                {
                    Name = BranchName,
                    Created = now,
                    CreatorUserId = admin.Id,
                };
                await _context.Branches.AddAsync(main);

                var de = new DomainEvent<Branch>()
                {
                    BranchId = main.Id,
                    EntityId = main.Id,
                    Date = now,
                    EventAction = Domain.Enums.EventAction.Insert,
                    EventArea = Domain.Enums.ApplicationArea.Dictionary,
                    UserId = admin.Id,
                    Entity = main
                };
                var pe = _eventConverter.ToPersistEvent(de);
                await _context.Events.AddAsync(pe);

                await _context.SaveChangesAsync();
            }

            var settings = await _context.Settings.FirstOrDefaultAsync();
            if (settings == null)
            {
                settings = new Settings()
                {
                    BranchId = main.Id,
                };
                await _context.Settings.AddAsync(settings);
                await _context.SaveChangesAsync();
            }
        }
    }
}