using MoneyMe.Application.ViewModel;

namespace MoneyMe.Web.Service.Query.Interface
{
    public interface IGetRepaymentAmountQuery
    {
        ResponseViewModel<double> GetRepaymentAmount(int mortgage, int months, string product);
    }
}
