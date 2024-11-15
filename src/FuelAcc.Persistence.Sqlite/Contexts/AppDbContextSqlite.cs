using FuelAcc.Domain.Entities.Documents;
using FuelAcc.Domain.Entities.Registry;
using FuelAcc.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace FuelAcc.Persistence.Sqlite.Contexts;

public class AppDbContextSqlite : AppDbContext
{
    public AppDbContextSqlite(DbContextOptions<AppDbContextSqlite> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // here you can customize database-specific model aspects

        modelBuilder.Entity<OrderBase>(e =>
        {
            e.Property(p => p.Total)
                .HasConversion<double>();
        });

        modelBuilder.Entity<OrderLine>(e =>
        {
            e.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();
            e.Property(p => p.Quantity)
                .HasConversion<double>();
            e.Property(p => p.Price)
                .HasConversion<double>();
            e.Property(p => p.Sum)
                .HasConversion<double>();
        });

        modelBuilder.Entity<Transaction>(e =>
        {
            e.Property(p => p.Quantity)
                .HasConversion<double>();
        });

        modelBuilder.Entity<Rest>(e =>
        {
            e.Property(p => p.Quantity)
               .HasConversion<double>();
        });
    }
}