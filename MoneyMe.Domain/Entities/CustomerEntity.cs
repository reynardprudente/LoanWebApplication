using System;
using System.Collections.Generic;
using System.Text;
using MoneyMe.Application.Entities;

namespace MoneyMe.Domain.Entities
{
    public class CustomerEntity : BaseEntity
    {
        public int AmountRequired { get; set; }

        public int Term { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Mobile { get; set; }

        public string EmailAddress { get; set; }
    }
}
