using MoneyMe.Application.ViewModel;
using MoneyMe.Web.ViewModel;

namespace MoneyMe.Web.Service.Query.Interface
{
    public interface IGetCustomerQuery
    {
        ResponseViewModel<CustomerViewModel> GetCustomer(long id);
    }
}
