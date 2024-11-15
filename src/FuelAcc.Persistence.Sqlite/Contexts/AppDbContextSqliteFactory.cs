using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FuelAcc.Persistence.Sqlite.Contexts;

public class AppDbContextSqliteFactory : IDesignTimeDbContextFactory<AppDbContextSqlite>
{
    public AppDbContextSqlite CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContextSqlite>();

        optionsBuilder.UseSqlite("Data Source = ./db/fuelacc.db", opts =>
        {
        });

        return new AppDbContextSqlite(optionsBuilder.Options);
    }
}