using ColegioMozart.Application.ClassSchedule.Commands;
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
    public async Task<IActionResult> GetForClassroom([FromRoute] GetClassScheduleByClassroomId cmd)
    {
        var data = await Mediator.Send(cmd);
        return Ok(data);
    }

    /// <summary>
    /// Obtiene un listado de horarios de clases para un docente
    /// </summary>
    /// <returns></returns>
    [HttpGet("teacher/{TeacherId}")]
    public async Task<IActionResult> GetForTeacher([FromRoute] Guid TeacherId)
    {
        var data = await Mediator.Send(new GetClassScheduleByTeacherIdQuery() { TeacherId = TeacherId, Year = DateTime.Now.Year });
        return Ok(data);
    }


    /// <summary>
    /// Obtiene un listado de horarios de clases para un alumno
    /// </summary>
    /// <returns></returns>
    [HttpGet("student/{StudentId}")]
    public async Task<IActionResult> GetForStudent([FromRoute] Guid StudentId)
    {
        var data = await Mediator.Send(new GetClassScheduleByStudentIdQuery() { StudentId = StudentId, Year = DateTime.Now.Year });
        return Ok(data);
    }



    /// <summary>
    /// Registra un curso a un horario para un salón de clases
    /// </summary>
    /// <returns></returns>
    [HttpPost()]
    public async Task<IActionResult> Register(AddCourseScheduleToClassroomCommand cmd)
    {
        var data = await Mediator.Send(cmd);
        return Ok(data);
    }


    /// <summary>
    /// Registra un curso a un horario para un salón de clases
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(Guid Id)
    {
        var data = await Mediator.Send(new DeleteCourseFromClassScheduleByIdCommand { ClassScheduleId = Id });
        return Ok(data);
    }

}
