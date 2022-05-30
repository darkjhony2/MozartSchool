using ColegioMozart.Application.StudentClassroom.Queries.StudentsByClassroom;
using ColegioMozart.Application.Students.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers
{
    public class StudentClassroomController : RestApiControllerBase
    {

        /// <summary>
        /// Obtiene una lista de los estudiantes de un salón de clase
        /// </summary>
        /// <returns></returns>
        [HttpGet("{classroomId}/students")]
        public async Task<IActionResult> Get(Guid classroomId)
        {
            var data = await Mediator.Send(new GetAllStudentsByClassroomId() { ClassroomId = classroomId });
            return Ok(data);
        }


        /// <summary>
        /// Asigna a un estudiante un salón de clases
        /// </summary>
        /// <returns></returns>
        [HttpPut("{ClassroomId}/student/{StudentId}")]
        public async Task<IActionResult> PutUserIntoClassroom([FromRoute] AssociateUserToClassroomCommand cmd)
        {
            var data = await Mediator.Send(cmd);
            return Ok(data);
        }


    }
}
