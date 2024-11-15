using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FuelAcc.Persistence.PostgreSql.Contexts;

public class AppDbContextPostgreSqlFactory : IDesignTimeDbContextFactory<AppDbContextPostgreSql>
{
    public AppDbContextPostgreSql CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContextPostgreSql>();

        optionsBuilder.UseNpgsql("Host=localhost; Database=fuel-app; Username=postgres; Password=postgres", opts =>
        {
        });

        return new AppDbContextPostgreSql(optionsBuilder.Options);
    }
}