using ColegioMozart.Application.AttendanceStatus.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers
{
    public class AttendanceStatusController : RestApiControllerBase
    {

        /// <summary>
        /// Lista los estados para el registro de asistencia
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await Mediator.Send(new GetAllAttendanceStatusQuery());
            return Ok(data);
        }
    }
}
