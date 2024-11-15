using FuelAcc.Persistence.Contexts;
using FuelAcc.Persistence.PostgreSql.Contexts;
using FuelAcc.Persistence.Sqlite.Contexts;
using FuelAcc.Persistence.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FuelAcc.Persistence.DbSelector
{
    public static class ConfigureServices
    {
        public static void AddInjectionDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = configuration.GetValue("Provider", Provider.Sqlite.Name);

            if (provider == Provider.SqlServer.Name)
            {
                services.AddDbContext<AppDbContext, AppDbContextSqlServer>(options =>
                {
                    var connection = configuration.GetConnectionString(Provider.SqlServer.Name);

                    options.UseSqlServer(connection,
                        x => x.MigrationsAssembly(Provider.SqlServer.Assembly));
                });
            }
            else if (provider == Provider.Sqlite.Name)
            {
                services.AddDbContext<AppDbContext, AppDbContextSqlite>(options =>
                {
                    var connection = configuration.GetConnectionString(Provider.Sqlite.Name);

                    options.UseSqlite(connection,
                        x => x.MigrationsAssembly(Provider.Sqlite.Assembly));
                });
            }
            else if (provider == Provider.PostgreSql.Name)
            {
                services.AddDbContext<AppDbContext, AppDbContextPostgreSql>(options =>
                {
                    var connection = configuration.GetConnectionString(Provider.PostgreSql.Name);

                    options.UseNpgsql(connection,
                        x => x.MigrationsAssembly(Provider.PostgreSql.Assembly));
                });
            }
        }
    }
}