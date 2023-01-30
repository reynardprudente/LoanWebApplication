using Microsoft.EntityFrameworkCore.Metadata;

namespace MoneyMe.API.Model
{
    public class CustomerModel
    {
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
