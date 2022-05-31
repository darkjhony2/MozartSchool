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

        /// <summary>
        /// Lista todos los cursos 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var subjects = await Mediator.Send(new GetSubjectsQuery());
            return Ok(subjects);
        }


        /// <summary>
        /// Busca un curso por su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var subject = await Mediator.Send(new GetSubjectByIdQuery() { Id = id });
            return Ok(subject);
        }

        /// <summary>
        /// Registra un curso
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateSubjectCommand subject)
        {
            await Mediator.Send(subject);
            return Ok(subject);
        }

        /// <summary>
        /// Actualiza los datos de un curso
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ESubjectDTO subject)
        {
            await Mediator.Send(new UpdateSubjectCommand() { Id = id, Dto = subject });
            return Ok(subject);
        }

        /// <summary>
        /// Elimina un curso por su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteSubjectCommand() { Id = id });
            return Ok();
        }

    }
}
