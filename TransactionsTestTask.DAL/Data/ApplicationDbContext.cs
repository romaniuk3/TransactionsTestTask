using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TransactionsTestTask.DAL.Entities;

namespace TransactionsTestTask.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public async Task UpsertTransactionsAsync(IEnumerable<Transaction> transactions, ApplicationDbContext dbContext)
        {
            using var dbTransaction = dbContext.Database.BeginTransaction();
            dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Transactions ON");
            foreach (var transaction in transactions)
            {
                var existingTransaction = await Transactions.FindAsync(transaction.Id);

                if(existingTransaction != null)
                {
                    Entry(existingTransaction).CurrentValues.SetValues(transaction);
                } else
                {
                    await Transactions.AddAsync(transaction);
                }
            }

            await SaveChangesAsync();

            dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Transactions OFF");
            dbTransaction.Commit();
        }
    }
}
