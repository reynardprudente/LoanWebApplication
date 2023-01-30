using MoneyMe.Application.ViewModel;
using MoneyMe.Web.Models.Transaction;
using System.Threading.Tasks;

namespace MoneyMe.Web.Service.Command.Interface
{
    public interface IAddLoanCommand
    {
        Task<ResponseViewModel<bool>> AddLoan(QuoteModel request);
    }
}
