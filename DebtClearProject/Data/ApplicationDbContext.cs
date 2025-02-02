using DebtClearProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DebtClearProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserDebts> UserDebts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Debt> Debts { get; set; }

    }
}
