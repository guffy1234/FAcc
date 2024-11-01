using FuelAcc.Domain.Entities.Dictionaries;
using FuelAcc.Domain.Entities.Documents;
using FuelAcc.Domain.Entities.Other;
using FuelAcc.Domain.Entities.Registry;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace FuelAcc.Persistence.Contexts;

public class AppDbContext : AppIdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Partner> Partners { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Storage> Storages { get; set; }

    public DbSet<OrderBase> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<Rest> Rests { get; set; }

    public DbSet<PersistEvent> Events { get; set; }

    public DbSet<ReplictionPacket> ReplictionPackets { get; set; }

    public DbSet<Settings> Settings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrderIn>()
            .ToTable("OrdersIn");
        modelBuilder.Entity<OrderOut>()
            .ToTable("OrdersOut");
        modelBuilder.Entity<OrderMove>()
            .ToTable("OrdersMove");

        modelBuilder.Entity<Product>()
            .Property(p => p.Id)
            .HasValueGenerator<SequentialGuidValueGenerator>();
        modelBuilder.Entity<Partner>()
            .Property(p => p.Id)
            .HasValueGenerator<SequentialGuidValueGenerator>();
        modelBuilder.Entity<Branch>()
            .Property(p => p.Id)
            .HasValueGenerator<SequentialGuidValueGenerator>();
        modelBuilder.Entity<Storage>()
            .Property(p => p.Id)
            .HasValueGenerator<SequentialGuidValueGenerator>();

        modelBuilder.Entity<OrderBase>()
            .Property(p => p.Id)
            .HasValueGenerator<SequentialGuidValueGenerator>();
        modelBuilder.Entity<OrderLine>()
            .Property(p => p.Id)
            .HasValueGenerator<SequentialGuidValueGenerator>();

        modelBuilder.Entity<Transaction>()
            .Property(p => p.Id)
            .HasValueGenerator<SequentialGuidValueGenerator>();
        modelBuilder.Entity<Rest>()
            .Property(p => p.Id)
            .HasValueGenerator<SequentialGuidValueGenerator>();

        modelBuilder.Entity<PersistEvent>()
            .Property(p => p.Id)
            .HasValueGenerator<SequentialGuidValueGenerator>();
        modelBuilder.Entity<ReplictionPacket>()
            .Property(p => p.Id)
            .HasValueGenerator<SequentialGuidValueGenerator>();

        modelBuilder.Entity<Branch>()
            .HasMany(e => e.Storages)
            .WithOne(e => e.Branch)
            .HasForeignKey(e => e.BranchId)
            .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Storage>()
            .HasMany(e => e.Rests)
            .WithOne(e => e.Storage)
            .HasForeignKey(e => e.StorageId)
            .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Product>()
            .HasMany(e => e.Rests)
            .WithOne(e => e.Product)
            .HasForeignKey(e => e.ProductId)
            .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Product>()
            .HasMany(e => e.Transactions)
            .WithOne(e => e.Product)
            .HasForeignKey(e => e.ProductId)
            .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Rest>()
            .HasMany(e => e.InTransactions)
            .WithOne(e => e.Source)
            .HasForeignKey(e => e.SourceId)
            .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Rest>()
            .HasMany(e => e.OutTransactions)
            .WithOne(e => e.Destination)
            .HasForeignKey(e => e.DestinationId)
            .HasPrincipalKey(e => e.Id);
    }
}