using MoneyMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyMe.Infrastructure.Data.Interface
{
    public interface ICustomerRepository
    {
        CustomerEntity GetUserById(long id);

        Task<CustomerEntity> GetCustomerByDetails(CustomerEntity customer, CancellationToken cancellationToken);
    }
}
