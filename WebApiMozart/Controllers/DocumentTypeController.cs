using ColegioMozart.Application.DocumentTypes.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers
{
    public class DocumentTypeController : RestApiControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await Mediator.Send(new GetAllDocumentTypesQuery());
            return Ok(data);
        }
    }
}
