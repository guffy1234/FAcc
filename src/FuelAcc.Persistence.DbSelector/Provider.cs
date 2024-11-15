using FuelAcc.Persistence.PostgreSql.Contexts;
using FuelAcc.Persistence.Sqlite.Contexts;
using FuelAcc.Persistence.SqlServer.Contexts;

namespace FuelAcc.Persistence.DbSelector
{
    public record Provider(string Name, string Assembly)
    {
        public static readonly Provider Sqlite = new(nameof(Sqlite), typeof(AppDbContextSqlite).Assembly.GetName().Name!);
        public static readonly Provider SqlServer = new(nameof(SqlServer), typeof(AppDbContextSqlServer).Assembly.GetName().Name!);
        public static readonly Provider PostgreSql = new(nameof(PostgreSql), typeof(AppDbContextPostgreSql).Assembly.GetName().Name!);
    }
}