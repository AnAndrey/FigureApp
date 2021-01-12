using Figure.Contracts.Db;
using Microsoft.EntityFrameworkCore;

namespace Figure.SqliteDb
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        private const string ConnectionString = "Data Source=../blogging.db";
        public DbSet<FigureRecord> Figures { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options) 
            => options.UseSqlite(ConnectionString);
    }
}
