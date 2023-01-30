using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MoneyMe.API.Model;
using MoneyMe.Application.Command.Interface;
using MoneyMe.Application.Dto;
using MoneyMe.Application.Enum;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyMe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly IAddCustomerCommand addCustomerCommand;

        public CustomerController(IAddCustomerCommand addCustomerCommand)
        {
            this.addCustomerCommand = addCustomerCommand ?? throw new ArgumentNullException(nameof(addCustomerCommand));
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerModel customer, CancellationToken cancellationToken)
        {
            try
            {
                var dto = Mapper.Map<CustomerDTO>(customer);
                var result = await this.addCustomerCommand.AddCustomer(dto, cancellationToken);
                if(result.Status == Status.Error)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Critical, $"trace stack: {ex.Message}, {ex.InnerException}");
                return BadRequest(ErrorResource.General_Error);
            }
        }
    }
}
