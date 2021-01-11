using Figure.Contracts.Db;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
