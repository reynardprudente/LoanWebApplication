using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MoneyMe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> AddCustomer()
        {
            return Ok();
        }
    }
}
