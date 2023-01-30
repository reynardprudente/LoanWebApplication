using Microsoft.EntityFrameworkCore.Storage;
using MoneyMe.Infrastructure.Data.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyMe.Infrastructure.Data
{
    public class GenericRepository : IGenericRepository
    {
        private readonly DatabaseContext databaseContext;
        public GenericRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }
        public void Add<TEntity>(TEntity entity)
        {
            this.databaseContext.Add(entity);
        }

        public void SaveChanges()
        {
            this.databaseContext.SaveChanges();
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return this.databaseContext.Database.BeginTransactionAsync(cancellationToken);
        }
    }
}
