using Microsoft.EntityFrameworkCore;

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

        public DbSet<Expense> Expenses { get; set; }
    }
}