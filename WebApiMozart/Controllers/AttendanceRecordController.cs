using ColegioMozart.Application.AttendanceRecord.Commands;
using ColegioMozart.Application.AttendanceRecord.Dtos;
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


        /// <summary>
        /// Registra el control de asistencia para un salón y una fecha
        /// </summary>
        /// <returns></returns>
        [HttpPost("classroom/{ClassroomId}/date/{Date}")]
        public async Task<IActionResult> RegisterAttendanceForClassroom(
            [FromRoute] Guid ClassroomId,
            [FromRoute] DateTime Date,
            [FromBody] List<RegisterAttendaceRecordResource> studentsAttendance)
        {
            var data = await Mediator.Send(new RegisterAttendanceRecordForClassroomCommand
            {
                ClassroomId = ClassroomId,
                Date = Date,
                StudentsAttendance = studentsAttendance
            });
            return Ok(data);
        }


        /// <summary>
        /// Actualiza la asistencia para un alumno en un día específico
        /// </summary>
        /// <returns></returns>
        [HttpPut("date/{Date}")]
        public async Task<IActionResult> UpdateAttendanceRecordForStudent(
            [FromRoute] DateTime Date,
            [FromBody] RegisterAttendaceRecordResource studentsAttendance)
        {
            var data = await Mediator.Send(new UpdateAttendanceRecordForStudentCommand
            {
                Date = Date,
                Resource = studentsAttendance
            });
            return Ok(data);
        }
        //

    }
}
