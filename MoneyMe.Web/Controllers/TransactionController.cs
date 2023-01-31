using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyMe.Application.Dto;
using MoneyMe.Application.Enum;
using MoneyMe.Web.Models.Transaction;
using MoneyMe.Web.Service.Command.Interface;
using MoneyMe.Web.Service.Query.Interface;
using System;

namespace MoneyMe.Web.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly IGetCustomerQuery getCustomerQuery;
        private readonly IGetRepaymentAmountQuery getRepaymentAmountQuery;
        private readonly IAddLoanCommand addLoanCommand;

        public TransactionController(ILogger<TransactionController> logger, IGetCustomerQuery getCustomerQuery,
            IGetRepaymentAmountQuery getRepaymentAmountQuery,
            IAddLoanCommand addLoanCommand)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(getCustomerQuery));
            this.getCustomerQuery = getCustomerQuery ?? throw new ArgumentNullException(nameof(getCustomerQuery));
            this.getRepaymentAmountQuery = getRepaymentAmountQuery ?? throw new ArgumentNullException(nameof(getRepaymentAmountQuery));
            this.addLoanCommand = addLoanCommand ?? throw new ArgumentNullException(nameof(addLoanCommand)); ;
        }
        
        public IActionResult CalculateQuote(long id)
        {
            try
            {
                var result = this.getCustomerQuery.GetCustomer(id);
                if(result.Status == Status.Error)
                {
                    return Json(new { status = "error", message = result.Message });
                }
                var model = new QuoteModel()
                {
                    Customer = new CustomerDTO()
                    {
                        Id = id,
                        AmountRequired = result.Value.AmountRequired,
                        Title = result.Value.Title,
                        FirstName = result.Value.FirstName,
                        LastName = result.Value.LastName,
                        Email = result.Value.EmailAddress,
                        DateOfBirth = result.Value.DateOfBirth.ToString("MM/dd/yyyy"),
                        Mobile = result.Value.Mobile,
                        Term = result.Value.Term,
                    }
                };
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Critical, $"trace stack: {ex.Message}, {ex.InnerException}");
                return Json(new { status = "error", message = ex.Message });
            }          
        }

        public IActionResult YourQuote(QuoteModel request)
        {
            try
            {
                request.Months = Convert.ToInt32(Request.Form["Months"]);
                request.Customer.AmountRequired = Convert.ToInt32(Request.Form["Amount"]);
                var result = getRepaymentAmountQuery.GetRepaymentAmount(request.Customer.AmountRequired, request.Months, request.Product);
                if(result.Status == Status.Error)
                {
                    return Json(new { status = "error", message = result.Message });
                }
                request.Repayment = result.Value;
                request.Name = $"{request.Customer.FirstName} {request.Customer.LastName}";
                return View(request);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Critical, $"trace stack: {ex.Message}, {ex.InnerException}");
                return Json(new { status = "error", message = ex.Message });
            }
        }

        public IActionResult ApplyNow(QuoteModel request)
        {
            try
            {
                var result = this.addLoanCommand.AddLoan(request).GetAwaiter().GetResult();
                if(result.Status == Status.Error)
                {
                    return Json(new { status = "error", message = result.Message });
                }
                return RedirectToAction("SuccessPage");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Critical, $"trace stack: {ex.Message}, {ex.InnerException}");
                return Json(new { status = "error", message = ex.Message });
            }
        }

        public IActionResult SuccessPage()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Critical, $"trace stack: {ex.Message}, {ex.InnerException}");
                return Json(new { status = "error", message = ex.Message });
            }
        }

        ///Not implemented because the details is kind a vague 
        public IActionResult EditInformation()
        {
            return View();
        }

        ///Not implemented because the details is kind a vague 
        public IActionResult EditFinanceDetails()
        {
            return View();
        }
    }
}
