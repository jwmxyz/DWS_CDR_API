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
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "cdr.db");
        }

        public CdrDbContext(DbContextOptions<CdrDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}