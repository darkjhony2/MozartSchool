using ColegioMozart.Application.AcademicLevels.Commands.CreateAcademicLevel;
using ColegioMozart.Application.AcademicLevels.Queries.GetAcademicLevelById;
using ColegioMozart.Application.AcademicLevels.Queries.GetAcademicLevels;
using ColegioMozart.Application.AcademicScale.Commands;
using ColegioMozart.Application.AcademicScale.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers
{

    /// <summary>
    /// Grados
    /// </summary>
    public class AcademicLevelController : RestApiControllerBase
    {

        /// <summary>
        /// Listar todos los grados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await Mediator.Send(new GetAcademicLevelsQuery());
            return Ok(data);
        }

        /// <summary>
        /// Obtener grado por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await Mediator.Send(new GetAcademicLevelByIdQuery() { Id = id });
            return Ok(data);
        }

        /// <summary>
        /// Lista los niveles academicos por escala academica
        /// </summary>
        /// <param name="academicScaleId"></param>
        /// <returns></returns>
        [HttpGet("AcademicScale/{academicScaleId}")]
        public async Task<IActionResult> GetByAcademicScaleId([FromRoute] int academicScaleId)
        {
            var data = await Mediator.Send(new GetAcademicLevelByAcademicScaleId() { AcademicScaleId = academicScaleId });
            return Ok(data);
        }

        /// <summary>
        /// Crear grado
        /// </summary>
        /// <param name="academicLevel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAcademicLevelCommand academicLevel)
        {
            await Mediator.Send(academicLevel);
            return Ok(academicLevel);
        }


        /// <summary>
        /// Eliminar grado
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteAcademicLevelCommand() { Id = id });
            return Ok();
        }

    }
}
