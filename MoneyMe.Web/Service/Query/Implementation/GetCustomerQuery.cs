using AutoMapper;
using MoneyMe.Application.Enum;
using MoneyMe.Application.ViewModel;
using MoneyMe.Infrastructure.Data.Interface;
using MoneyMe.Web.Service.Query.Interface;
using MoneyMe.Web.ViewModel;
using System;

namespace MoneyMe.Web.Service.Query.Implementation
{
    public class GetCustomerQuery : IGetCustomerQuery
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public GetCustomerQuery(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public ResponseViewModel<CustomerViewModel> GetCustomer(long id)
        {
            try
            {
                var result = this.customerRepository.GetUserById(id);
                if(result == null)
                {
                    return new ResponseViewModel<CustomerViewModel>()
                    {
                        Status = Status.Error,
                        Message = "No customer found",
                        Value = null
                    };
                }
                return new ResponseViewModel<CustomerViewModel>()
                {
                    Status = Status.Success,
                    Message = "Customer found",
                    Value = this.mapper.Map<CustomerViewModel>(result)
                };
            }
            catch (Exception ex)
            {
                throw new Exception(Error.GetCustomerError, ex);
            }
        }
    }
}
