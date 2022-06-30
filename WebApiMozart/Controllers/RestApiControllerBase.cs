using ColegioMozart.Application.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class RestApiControllerBase : ControllerBase
    {

        private ISender _mediator = null!;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
