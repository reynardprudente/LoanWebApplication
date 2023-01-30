using MoneyMe.Application.Dto;

namespace MoneyMe.Web.Models.Transaction
{
    public class QuoteModel
    {
        public CustomerDTO Customer { get; set; }

        public int Months { get; set; }

        public string Product { get; set; }

        public double Repayment { get; set; }

        public string Name { get; set; }
    }
}
