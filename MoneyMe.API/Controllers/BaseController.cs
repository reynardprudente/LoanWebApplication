using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace MoneyMe.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected ILogger Logger => HttpContext.RequestServices.GetService<ILogger>() ?? NullLogger.Instance;

        protected IMapper Mapper => HttpContext.RequestServices.GetService<IMapper>();
    }
}
