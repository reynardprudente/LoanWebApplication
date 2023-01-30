using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyMe.Application.Dto
{
    public class CustomerDTO
    {
        public long Id { get; set; }

        public int AmountRequired { get; set; }

        public int Term { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DateOfBirth { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }
    }
}
