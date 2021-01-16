using Figure.Contracts.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Figure.SqliteDb.Extensions
{
    public static class DbExtensions 
    {
        public static IServiceCollection AddDb(this IServiceCollection services)
        {
            return services
                .AddEntityFrameworkSqlite()
                .AddDbContext<DatabaseContext>()
                .AddScoped<IDatabaseContext>(_ => new DatabaseContext()); ;
        }
    }

}
