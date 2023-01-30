using MoneyMe.Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyMe.Domain.Entities
{
    public class TransactionEntity : BaseEntity
    {
        public CustomerEntity Customer { get; set; }

        public double RepaymentAmount { get; set; }

        public string Product { get; set; }
    }
}
