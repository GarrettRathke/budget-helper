using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.Globalization;

namespace Budget.Data
{
    public class BudgetDataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public BudgetDataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Configuration.GetConnectionString("BudgetDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
                .ToTable("Expenses");

            using (var reader = new StreamReader(Directory.GetCurrentDirectory() + "/Data/MOCK_DATA.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Expense>();
                modelBuilder.Entity<Expense>().HasData(records);
            }
        }

        public DbSet<Expense> Expenses { get; set; }
    }
}