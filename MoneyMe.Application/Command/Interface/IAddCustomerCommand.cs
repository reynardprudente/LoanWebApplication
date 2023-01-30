using MoneyMe.Application.Dto;
using MoneyMe.Application.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyMe.Application.Command.Interface
{
    public interface IAddCustomerCommand
    {
        Task<ResponseViewModel<string>> AddCustomer(CustomerDTO customer, CancellationToken cancellation);
    }
}
