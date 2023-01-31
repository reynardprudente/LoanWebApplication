using MoneyMe.Application.Enum;
using MoneyMe.Application.ViewModel;
using MoneyMe.Web.Service.Query.Interface;
using System;

namespace MoneyMe.Web.Service.Query.Implementation
{
    public class GetRepaymentAmountQuery : IGetRepaymentAmountQuery
    {
        const double rate = 0.475;
        const double apr = rate * 12;
        public ResponseViewModel<double> GetRepaymentAmount(int mortgage, int months, string product)
        {
            try
            {
                double repaymentAmount = 0;
                switch (product)
                {
                    case "ProductA":
                        {
                            repaymentAmount = Math.Round((double)mortgage / months, 2);
                            break;
                        }
                    case "ProductB":
                        {
                            var rate = (double)apr / 100 / 14;
                            var denominator = Math.Pow((1 + rate), months + 2) - 1;
                            repaymentAmount = Math.Round((rate + (rate / denominator)) * mortgage, 2);
                            break;
                        }
                    case "ProductC":
                        {
                            var rate = (double)apr / 100 / 12;
                            var denominator = Math.Pow((1 + rate), months) - 1;
                            repaymentAmount = Math.Round((rate + (rate / denominator)) * mortgage, 2);
                            break;
                        }
                    default:
                        {
                            return new ResponseViewModel<double>()
                            {
                                Status = Status.Error,
                                Value = 0,
                                Message = "product is not on the options"
                            };
                        }
                }
                return new ResponseViewModel<double>()
                {
                    Status = Status.Success,
                    Value = repaymentAmount,
                    Message = "Repayment computed"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(Error.GetRepaymentAmount, ex);
            }
        }
    }
}
