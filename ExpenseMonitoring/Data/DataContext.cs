using ExpenseMonitoring.Model;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMonitoring.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
