using ColegioMozart.Application.AttendanceRecord.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers
{
    public class AttendanceRecordController : RestApiControllerBase
    {
        /// <summary>
        /// Lista el control de asistencia de los alumnos de un salón y fecha específica
        /// </summary>
        /// <returns></returns>
        [HttpGet("classroom/{ClassroomId}/date/{Date}")]
        public async Task<IActionResult> GetByClassroom([FromRoute] GetAttendanceRecordsByDateAndClassroomQuery query)
        {
            var data = await Mediator.Send(query);
            return Ok(data);
        }

        /// <summary>
        /// Lista el control de asistencia de un alumno en un periodo académico
        /// </summary>
        /// <returns></returns>
        [HttpGet("academicPeriod/{AcademicPeriodId}/student/{StudentId}")]
        public async Task<IActionResult> GetByStudent([FromRoute] GetAttendanceRecordByAcademicPeriodAndStudentQuery query)
        {
            var data = await Mediator.Send(query);
            return Ok(data);
        }

    }
}
