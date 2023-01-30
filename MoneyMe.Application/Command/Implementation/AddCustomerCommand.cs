using AutoMapper;
using MoneyMe.Application.Command.Interface;
using MoneyMe.Application.Dto;
using MoneyMe.Application.Enum;
using MoneyMe.Application.ViewModel;
using MoneyMe.Domain.Entities;
using MoneyMe.Infrastructure.Data;
using MoneyMe.Infrastructure.Data.Interface;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyMe.Application.Command.Implementation
{
    public class AddCustomerCommand : IAddCustomerCommand
    {
        private readonly IGenericRepository genericRepository;
        private readonly IMapper mapper;
        private readonly ICustomerRepository customerRepository;
        const string Url = "http://localhost:5000/Transaction/CalculateQuote/{0}";

        public AddCustomerCommand(IGenericRepository genericRepository, IMapper mapper,
           ICustomerRepository customerRepository)
        {
            this.genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }
        public  async Task<ResponseViewModel<string>> AddCustomer(CustomerDTO request, CancellationToken cancellationToken)
        {
            try
            {
                request = request ?? throw new ArgumentNullException(nameof(request));
                await using var transaction = await this.genericRepository.BeginTransactionAsync(cancellationToken);

                var customer = this.mapper.Map<CustomerEntity>(request);

                var customerFrmDB = await this.customerRepository.GetCustomerByDetails(customer, cancellationToken);
                if(customerFrmDB != null)
                {
                    return new ResponseViewModel<string>()
                    {
                        Status = Status.Success,
                        Value =   string.Format(Url, customerFrmDB.Id.ToString()) ,
                        Message = "Customer already register" 
                    };
                }

                this.genericRepository.Add(customer);

                this.genericRepository.SaveChanges();
                await transaction.CommitAsync();

                return new ResponseViewModel<string>()
                {
                    Status = Status.Success,
                    Value = string.Format(Url,customer.Id.ToString()),
                    Message = "Customer successfully inserted"
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ErrorResource.AddCustomerError, ex);
            }
        }
    }
}
