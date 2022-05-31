using ColegioMozart.Application.Teachers.Commands;
using ColegioMozart.Application.Teachers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers
{
    public class TeacherController : RestApiControllerBase
    {
        /// <summary>
        /// Lista todos los docentes de la institución educativa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await Mediator.Send(new GetAllTeachersQuery());
            return Ok(data);
        }

        /// <summary>
        /// Obtiene un docente por su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new GetTeacherByIdQuery() { Id = id });
            return Ok(data);
        }

        /// <summary>
        /// Registra un nuevo docente
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTeacherCommand data)
        {
            await Mediator.Send(data);
            return Ok(data);
        }
    }
}
