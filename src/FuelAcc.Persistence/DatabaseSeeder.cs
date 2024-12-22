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
            //warning: do not change order of seeding procedures!
            await SeedIdentityAsync();
            await SeedBranchAndSettingsAsync();
            await SeedRootFoldersAsync();
        }

        private async Task SeedIdentityAsync()
        {
            if (await _roleManager.FindByNameAsync(Constants.Constants.AdminRoleName) == null)
            {
                var adminRole = new ApplicationRole()
                {
                    Name = Constants.Constants.AdminRoleName
                };
                await _roleManager.CreateAsync(adminRole);
                await _roleManager.AddClaimAsync(adminRole, new Claim(Constants.Constants.AdminRoleName, true.ToString()));
            }
            if (await _roleManager.FindByNameAsync(Constants.Constants.EmployeeRoleName) == null)
            {
                var employeeRole = new ApplicationRole()
                {
                    Name = Constants.Constants.EmployeeRoleName
                };
                var result = await _roleManager.CreateAsync(employeeRole);
                if (result.Succeeded)
                {
                    employeeRole = await _roleManager.FindByNameAsync(Constants.Constants.EmployeeRoleName);
                    var claims = ClaimsHelper.MakeAllClaims();
                    foreach (var claim in claims)
                    {
                        await _roleManager.AddClaimAsync(employeeRole, claim);
                    }
                }
            }
            if (await _userManager.FindByNameAsync(Constants.Constants.AdminUserName) == null)
            {
                var admin = new ApplicationUser { Email = Constants.Constants.AdminEmail, UserName = Constants.Constants.AdminUserName };
                var result = await _userManager.CreateAsync(admin, Constants.Constants.AdminPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, Constants.Constants.AdminRoleName);
                    var claims = ClaimsHelper.MakeAllClaims();
                    await _userManager.AddClaimsAsync(admin, claims);
                }
            }
        }

        private async Task SeedBranchAndSettingsAsync()
        {
            var admin = await _context.Users.FirstOrDefaultAsync(u => u.UserName == Constants.Constants.AdminUserName);
            var now = DateTime.UtcNow;

            var main = await _context.Branches.FirstOrDefaultAsync();
            if (main == null)
            {
                main = new Branch()
                {
                    Name = Constants.Constants.BranchName,
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

        private async Task SeedRootFoldersAsync()
        {
            var admin = await _context.Users.FirstOrDefaultAsync(u => u.UserName == Constants.Constants.AdminUserName);
            var now = DateTime.UtcNow;

            var settings = await _context.Settings.FirstOrDefaultAsync();
            if (settings == null)
            {
                throw new InvalidOperationException("No settings before seed root folders");
            }
            var mainBranchId = settings.BranchId;

            var added = false;

            foreach (var root in Constants.Constants.Roots)
            {
                var folder = await _context.Folders.FirstOrDefaultAsync(u => u.Id == root.Key);
                if (folder != null)
                {
                    continue;
                }
                folder = new Folder
                {
                    Id = root.Key,
                    Name = root.Value,
                    Created = now,
                    CreatorUserId = admin.Id,
                };

                await _context.Folders.AddAsync(folder);

                var de = new DomainEvent<Folder>()
                {
                    BranchId = mainBranchId,
                    EntityId = folder.Id,
                    Date = now,
                    EventAction = Domain.Enums.EventAction.Insert,
                    EventArea = Domain.Enums.ApplicationArea.Dictionary,
                    UserId = admin.Id,
                    Entity = folder,
                };
                var pe = _eventConverter.ToPersistEvent(de);
                await _context.Events.AddAsync(pe);
            }

            if (added)
            {
                await _context.SaveChangesAsync();
            }
        }
    }
}