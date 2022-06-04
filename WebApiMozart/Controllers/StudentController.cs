using ColegioMozart.Application.Students.Commands;
using ColegioMozart.Application.Students.Dtos;
using ColegioMozart.Application.Students.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers
{
    public class StudentController : RestApiControllerBase
    {
        /// <summary>
        /// Listar todos los estudiantes de la institución educactiva
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await Mediator.Send(new GetAllStudentsQuery());
            return Ok(data);
        }

        /// <summary>
        /// Obtiene el detalle de un estudiante por su Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetail([FromRoute] Guid id)
        {
            var data = await Mediator.Send(new GetStudentByIdQuery() { StudentId = id });
            return Ok(data);
        }


        /// <summary>
        /// Listar los estudiantes por el grado (nivel académico)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("academicLevel/{id}")]
        public async Task<IActionResult> GetByCurrentLevel(int id)
        {
            var data = await Mediator.Send(new GetStudentsByAcademicLevelQuery() { AcademicLevelId = id });
            return Ok(data);
        }

        /// <summary>
        /// Registra un nuevo estudiante
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> RegisterStudent([FromBody] CreateStudentCommand cmd)
        {
            var data = await Mediator.Send(cmd);
            return Ok(data);
        }

        /// <summary>
        /// Actualiza los datos de un estudiante
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] Guid id, [FromBody] UpdateStudentResource resource)
        {
            var data = await Mediator.Send(new UpdateStudentPersonalDataCommand { Resource = resource, StudentId = id });
            return Ok(data);
        }

    }
}
