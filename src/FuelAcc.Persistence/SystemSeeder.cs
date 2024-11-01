using FuelAcc.Domain.Entities.Dictionaries;
using FuelAcc.Domain.Entities.Other;
using FuelAcc.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FuelAcc.Persistence.Repositories
{
    public class SystemSeeder
    {
        private const string BranchName = "Main";

        public static async Task SeedAsync(AppDbContext context)
        {
            var main = await context.Branches.FirstOrDefaultAsync();
            if (main == null)
            {
                main = new Branch()
                {
                    Name = BranchName,
                };
                await context.Branches.AddAsync(main);
                await context.SaveChangesAsync();
            }

            var settings = await context.Settings.FirstOrDefaultAsync();
            if (settings == null)
            {
                settings = new Settings()
                {
                    BranchId = main.Id,
                };
                await context.Settings.AddAsync(settings);
                await context.SaveChangesAsync();
            }
        }
    }
}