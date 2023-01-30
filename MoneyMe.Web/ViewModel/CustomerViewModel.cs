using MoneyMe.Application.Enum;
using System;

namespace MoneyMe.Web.ViewModel
{
    public class CustomerViewModel
    {
        public long Id { get; set; }

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
