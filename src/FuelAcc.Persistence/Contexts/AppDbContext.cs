using FuelAcc.Domain.Entities.Dictionaries;
using FuelAcc.Domain.Entities.Documents;
using FuelAcc.Domain.Entities.Other;
using FuelAcc.Domain.Entities.Registry;
using FuelAcc.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace FuelAcc.Persistence.Contexts;

public class AppDbContext : AppIdentityDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Partner> Partners { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<FileBlob> FileBlobs { get; set; }
    public DbSet<Folder> Folders { get; set; }

    public DbSet<OrderBase> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    public DbSet<OrderPropertyLine> OrderProperties { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<Rest> Rests { get; set; }

    public DbSet<PersistEvent> Events { get; set; }

    public DbSet<ReplictionPacket> ReplictionPackets { get; set; }

    public DbSet<Settings> Settings { get; set; }

    public DbSet<PropertyLineDefault> PropertyDefaults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(e =>
        {
            e.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();
            e.Property(p => p.Name)
                .HasMaxLength(256);
            e.HasMany(e => e.Rests)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasMany(e => e.Transactions)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasMany(e => e.OrderLines)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Partner>(e =>
        {
            e.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();
            e.Property(p => p.Name)
                .HasMaxLength(256);
        });

        modelBuilder.Entity<Branch>(e =>
        {
            e.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();
            e.Property(p => p.Name)
                .HasMaxLength(256);
            e.HasMany(e => e.Storages)
                .WithOne(e => e.Branch)
                .HasForeignKey(e => e.BranchId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Storage>(e =>
        {
            e.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();
            e.Property(p => p.Name)
                .HasMaxLength(256);
            e.HasMany(e => e.Rests)
                .WithOne(e => e.Storage)
                .HasForeignKey(e => e.StorageId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Folder>(e =>
        {
            e.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();
            e.Property(p => p.Name)
                .HasMaxLength(256);
        });

        modelBuilder.Entity<FileBlob>(e =>
        {
            e.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();
            e.Property(p => p.Name)
                .HasMaxLength(256);
            e.Property(p => p.FileName)
               .HasMaxLength(256);
            e.Property(p => p.MimeType)
               .HasMaxLength(256);
            e.Property(p => p.SHA256)
               .HasMaxLength(64);

            e.HasMany(e => e.Products)
                .WithMany(b => b.Blobs)
                .UsingEntity("ProductFileBlob",
                    l => l.HasOne(typeof(Product)).WithMany().HasForeignKey("ProductId").HasPrincipalKey(nameof(Product.Id)),
                    r => r.HasOne(typeof(FileBlob)).WithMany().HasForeignKey("FileBlobId").HasPrincipalKey(nameof(FileBlob.Id)),
                    j => j.HasKey("ProductId", "FileBlobId")); 

            e.HasMany(e => e.Orders)
                .WithMany(b => b.Blobs)
                .UsingEntity("OrderBaseFileBlob",
                    l => l.HasOne(typeof(OrderBase)).WithMany().HasForeignKey("OrderBaseId").HasPrincipalKey(nameof(OrderBase.Id)),
                    r => r.HasOne(typeof(FileBlob)).WithMany().HasForeignKey("FileBlobId").HasPrincipalKey(nameof(FileBlob.Id)),
                    j => j.HasKey("OrderBaseId", "FileBlobId")); ;
        });

        modelBuilder.Entity<OrderBase>(e =>
        {
            e.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();
            e.Property(p => p.Number)
               .HasMaxLength(512);
            e.Property(p => p.Description)
               .HasMaxLength(2048);
            e.Property(p => p.Total)
                .HasPrecision(14, 2);
        });

        modelBuilder.Entity<OrderIn>(e =>
        {
            e.ToTable("OrdersIn");
        });
        modelBuilder.Entity<OrderOut>(e =>
        {
            e.ToTable("OrdersOut");
        });
        modelBuilder.Entity<OrderMove>(e =>
        {
            e.ToTable("OrdersMove");
        });

        modelBuilder.Entity<OrderLine>(e =>
        {
            e.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();
            e.Property(p => p.PlannedQuantity)
                .HasPrecision(14, 3);
            e.Property(p => p.Quantity)
                .HasPrecision(14, 3);
            e.Property(p => p.Price)
                .HasPrecision(14, 5);
            e.Property(p => p.Sum)
                .HasPrecision(14, 5);
        });

        modelBuilder.Entity<OrderPropertyLine>(e =>
        {
            e.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();
            e.Property(p => p.Name)
                .HasMaxLength(256);
            e.Property(p => p.Value)
                .HasMaxLength(4096);
        });

        modelBuilder.Entity<PropertyLineDefault>(e =>
        {
            e.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();
            e.Property(p => p.Name)
                .HasMaxLength(256);
            e.Property(p => p.Area)
                .HasMaxLength(256);
            e.Property(p => p.Value)
                .HasMaxLength(4096);
        });

        modelBuilder.Entity<Transaction>(e =>
        {
            e.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();
            e.Property(p => p.Quantity)
                .HasPrecision(14, 3);
            e.Property(p => p.Price)
                .HasPrecision(14, 5);
        });

        modelBuilder.Entity<Rest>(e =>
        {
            e.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();
            e.Property(p => p.Quantity)
                .HasPrecision(14, 3);
            e.Property(p => p.Price)
                .HasPrecision(14, 5);
            e.HasMany(e => e.InTransactions)
                .WithOne(e => e.Source)
                .HasForeignKey(e => e.SourceId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasMany(e => e.OutTransactions)
                .WithOne(e => e.Destination)
                .HasForeignKey(e => e.DestinationId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PersistEvent>(e =>
        {
            e.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();
            e.Property(p => p.ObjectClass)
                .HasMaxLength(1024);
            e.Property(p => p.EventAction)
                .HasConversion(new EnumToStringConverter<EventAction>())
                .HasMaxLength(128);
            e.Property(p => p.EventArea)
                .HasConversion(new EnumToStringConverter<ApplicationArea>())
                .HasMaxLength(128);
        });

        modelBuilder.Entity<ReplictionPacket>()
            .Property(p => p.Id)
            .HasValueGenerator<SequentialGuidValueGenerator>();
    }
}