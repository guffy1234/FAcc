using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FuelAcc.Persistence.Contexts;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        //optionsBuilder.UseSqlite(@"Data Source = ./db/fuelacc.db", opts =>
        //{
        //    opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
        //});
        optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=FielAcc;Integrated Security=True", opts =>
        {
        });

        return new AppDbContext(optionsBuilder.Options);
    }
}