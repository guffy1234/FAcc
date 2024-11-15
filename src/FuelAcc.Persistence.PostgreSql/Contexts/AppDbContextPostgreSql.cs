using FuelAcc.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FuelAcc.Persistence.PostgreSql.Contexts;

public class AppDbContextPostgreSql : AppDbContext
{
    public AppDbContextPostgreSql(DbContextOptions<AppDbContextPostgreSql> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // here you can customize database-specific model aspects
    }
}