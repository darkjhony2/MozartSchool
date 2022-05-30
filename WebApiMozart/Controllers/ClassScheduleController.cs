using ColegioMozart.Application.ClassSchedule.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers;

public class ClassScheduleController : RestApiControllerBase
{

    /// <summary>
    /// Obtiene un listado de horarios de clases para un salón de clases
    /// </summary>
    /// <returns></returns>
    [HttpGet("classroom/{ClassroomId}")]
    public async Task<IActionResult> Get([FromRoute] GetClassScheduleByClassroomId cmd)
    {
        var data = await Mediator.Send(cmd);
        return Ok(data);
    }

}
