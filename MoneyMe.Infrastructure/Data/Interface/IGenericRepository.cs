using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MoneyMe.Infrastructure.Data.Interface
{
    public interface IGenericRepository
    {
        void Add<TEntity>(TEntity entity);

        void SaveChanges();

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    }
}
