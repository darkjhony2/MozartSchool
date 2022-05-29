using ColegioMozart.Application.Subjects.Commands.CreateSubject;
using ColegioMozart.Application.Subjects.Commands.DeleteSubject;
using ColegioMozart.Application.Subjects.Commands.UpdateSubject;
using ColegioMozart.Application.Subjects.Queries;
using ColegioMozart.Application.Subjects.Queries.GetSubjectById;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers
{
    public class SubjectController : RestApiControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var subjects = await Mediator.Send(new GetSubjectsQuery());
            return Ok(subjects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var subject = await Mediator.Send(new GetSubjectByIdQuery() { Id = id });
            return Ok(subject);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateSubjectCommand subject)
        {
            await Mediator.Send(subject);
            return Ok(subject);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ESubjectDTO subject)
        {
            await Mediator.Send(new UpdateSubjectCommand() { Id = id, Dto = subject });
            return Ok(subject);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteSubjectCommand() { Id = id });
            return Ok();
        }

    }
}
