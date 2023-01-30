using Microsoft.EntityFrameworkCore;
using MoneyMe.Domain.Entities;
using MoneyMe.Infrastructure.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyMe.Infrastructure.Data.Query
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseContext databaseContext;
        public CustomerRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        public CustomerEntity GetUserById(long id)
        {
            return databaseContext.Customers.Where(x => x.Id == id).FirstOrDefault();
        }


        public async Task<CustomerEntity> GetCustomerByDetails(CustomerEntity customer, CancellationToken cancellationToken)
        {
            return await databaseContext.Customers
                .Where(x => x.FirstName == customer.FirstName.Trim() && x.LastName == customer.LastName.Trim()
                && x.DateOfBirth == customer.DateOfBirth)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
