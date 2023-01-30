using MoneyMe.Application.Enum;
using MoneyMe.Application.ViewModel;
using MoneyMe.Domain.Entities;
using MoneyMe.Infrastructure.Data.Interface;
using MoneyMe.Web.Constant;
using MoneyMe.Web.Models.Transaction;
using MoneyMe.Web.Service.Command.Interface;
using MoneyMe.Web.Service.Query.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyMe.Web.Service.Command.Implementation
{
    public class AddLoanCommand : IAddLoanCommand
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IGenericRepository genericRepository;

        public AddLoanCommand(ICustomerRepository customerRepository, IGenericRepository genericRepository)
        {
            this.customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            this.genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
        }
        public async Task<ResponseViewModel<bool>> AddLoan(QuoteModel request)
        {
            try
            {
                var Dob = Convert.ToDateTime(request.Customer.DateOfBirth);
                var age = GetAge(Dob);
                bool isAgeValid = validateAge(age, Dob);
                if (!isAgeValid)
                {
                    return new ResponseViewModel<bool>()
                    {
                        Status = Status.Error,
                        Value = false,
                        Message = "Age is not valid"
                    };

                }
                bool isMobileValid = validate(true, request.Customer.Mobile, MobileNumbers.Invalid());
                if (!isMobileValid)
                {
                    return new ResponseViewModel<bool>()
                    {
                        Status = Status.Error,
                        Value = false,
                        Message = "Mobile is not valid"
                    };

                }
                bool isEmailValid = validate(false, request.Customer.Email, Email.Invalid());
                if (!isEmailValid)
                {
                    return new ResponseViewModel<bool>()
                    {
                        Status = Status.Error,
                        Value = false,
                        Message = "Email is not valid"
                    };

                }

                var customer = this.customerRepository.GetUserById(request.Customer.Id);
                using var transaction = await this.genericRepository.BeginTransactionAsync(CancellationToken.None);
                var operation = new TransactionEntity()
                {
                    Customer = customer,
                    Product = request.Product,
                    RepaymentAmount = request.Repayment
                };

                this.genericRepository.Add(operation);

                this.genericRepository.SaveChanges();
                await transaction.CommitAsync();

                return new ResponseViewModel<bool>()
                {
                    Status = Status.Success,
                    Value = true,
                    Message = "Success loan"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(Error.AddLoan, ex);
            }
        }

        private bool validate(bool isMobile, string item, List<string> invalidList)
        {
            if (isMobile)
            {
                foreach (var invalid in invalidList)
                {
                    if (item == invalid)
                    {
                        return false;
                    };
                }
            }
            foreach(var invalid in invalidList)
            {
                if (item.Contains(invalid))
                { 
                    return false;
                };
            }
            return true;
        }

        private bool validateAge(int age, DateTime dob)
        {
            if (age < 18)
            {
                return false;
            }
            else if (age == 17)
            {
                var monthNow = DateTime.Now.Month;
                var monthDob = dob.Month;
                if (monthNow == monthDob)
                {
                    var dayNow = DateTime.Now.Month;
                    var dayDob = dob.Day;
                    if (dayNow - 1 != dayDob)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private int GetAge(DateTime dateOfBirth)
        {
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int dob = int.Parse(dateOfBirth.ToString("yyyyMMdd"));
            int age = (now - dob) / 10000;
            return age;
        }
    }
}
