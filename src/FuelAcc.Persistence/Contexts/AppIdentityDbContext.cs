using FuelAcc.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace FuelAcc.Persistence.Contexts;

public class AppIdentityDbContext
    : IdentityDbContext<
        ApplicationUser, ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
{
    public AppIdentityDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(b =>
        {
            // Each User can have many UserClaims
            b.HasMany(e => e.Claims)
                .WithOne(e => e.User)
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            // Each User can have many UserLogins
            b.HasMany(e => e.Logins)
                .WithOne(e => e.User)
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            // Each User can have many UserTokens
            b.HasMany(e => e.Tokens)
                .WithOne(e => e.User)
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            // Each User can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        modelBuilder.Entity<ApplicationRole>(b =>
        {
            // Each Role can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            // Each Role can have many associated RoleClaims
            b.HasMany(e => e.RoleClaims)
                .WithOne(e => e.Role)
                .HasForeignKey(rc => rc.RoleId)
                .IsRequired();
        });

        const string IdentityTablesPrefix = "App";

        modelBuilder.Entity<ApplicationUser>(b =>
        {
            b.ToTable(IdentityTablesPrefix+"Users");
            b.HasKey(p => p.Id);
            b.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();
        });

        modelBuilder.Entity<ApplicationUserClaim>(b =>
        {
            b.ToTable(IdentityTablesPrefix + "UserClaims");
            b.HasKey(p => p.Id);
        });

        modelBuilder.Entity<ApplicationUserLogin>(b =>
        {
            b.ToTable(IdentityTablesPrefix + "UserLogins");
        });

        modelBuilder.Entity<ApplicationUserToken>(b =>
        {
            b.ToTable(IdentityTablesPrefix + "UserTokens");
        });

        modelBuilder.Entity<ApplicationRole>(b =>
        {
            b.ToTable(IdentityTablesPrefix + "Roles");
            b.HasKey(p => p.Id);
            b.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();
        });

        modelBuilder.Entity<ApplicationRoleClaim>(b =>
        {
            b.ToTable(IdentityTablesPrefix + "RoleClaims");
            b.HasKey(p => p.Id);
        });

        modelBuilder.Entity<ApplicationUserRole>(b =>
        {
            b.ToTable(IdentityTablesPrefix + "UserRoles");
        });
    }
}
