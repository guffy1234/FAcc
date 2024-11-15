using FuelAcc.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FuelAcc.Persistence.SqlServer.Contexts;

public class AppDbContextSqlServer : AppDbContext
{
    public AppDbContextSqlServer(DbContextOptions<AppDbContextSqlServer> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // here you can customize database-specific model aspects
    }
}