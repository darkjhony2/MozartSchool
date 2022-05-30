using ColegioMozart.Application.Teachers.Commands;
using ColegioMozart.Application.Teachers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers
{
    public class TeacherController : RestApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await Mediator.Send(new GetAllTeachersQuery());
            return Ok(data);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new GetTeacherByIdQuery() { Id = id });
            return Ok(data);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTeacherCommand data)
        {
            await Mediator.Send(data);
            return Ok(data);
        }
    }
}
