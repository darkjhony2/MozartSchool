using ColegioMozart.Application.ClassRoom.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers
{
    public class ClassroomController : RestApiControllerBase
    {

        /// <summary>
        /// Buscar salones de clase por año
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int year)
        {
            var data = await Mediator.Send(new GetAllClassRoomsByYear() { Year = year });
            return Ok(data);
        }

        /// <summary>
        /// Buscar salon de clase por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new GetClassroomByIdQuery() { Id = id });
            return Ok(data);
        }

    }
}
