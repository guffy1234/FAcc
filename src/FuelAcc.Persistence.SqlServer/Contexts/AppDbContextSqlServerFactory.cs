using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FuelAcc.Persistence.SqlServer.Contexts;

public class AppDbContextSqlServerFactory : IDesignTimeDbContextFactory<AppDbContextSqlServer>
{
    public AppDbContextSqlServer CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContextSqlServer>();

        optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=FielAcc;Integrated Security=True", opts =>
        {
        });

        return new AppDbContextSqlServer(optionsBuilder.Options);
    }
}