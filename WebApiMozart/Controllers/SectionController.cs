using ColegioMozart.Application.Sections.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers
{
    public class SectionController : RestApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await Mediator.Send(new GetSectionsQuery());
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await Mediator.Send(new GetSectionByIdQuery() { Id = id });
            return Ok(data);
        }

    }
}
