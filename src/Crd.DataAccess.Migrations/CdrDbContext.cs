using Crd.DataAccess.Migrations.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Crd.DataAccess.Migrations
{
    public class CdrDbContext : DbContext
    {
        public DbSet<CallRecord> CallRecords { get; set; }

        public string DbPath { get; }

        public CdrDbContext() : base()
        {
            var folder = Environment.CurrentDirectory;
            DbPath = Path.Join(folder, "cdr.db");
        }

        public CdrDbContext(DbContextOptions<CdrDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source={DbPath}");
            }
        }
    }
}