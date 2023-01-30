using Microsoft.EntityFrameworkCore;
using MoneyMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyMe.Infrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<CustomerEntity> Customers { get; set; }

        public DbSet<TransactionEntity> Transactions { get; set; }
    }
}
