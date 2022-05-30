using ColegioMozart.Application.AcademicLevels.Commands.CreateAcademicLevel;
using ColegioMozart.Application.AcademicLevels.Queries.GetAcademicLevelById;
using ColegioMozart.Application.AcademicLevels.Queries.GetAcademicLevels;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await Mediator.Send(new GetAcademicLevelByIdQuery() { Id = id });
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAcademicLevelCommand academicLevel)
        {
            await Mediator.Send(academicLevel);
            return Ok(academicLevel);
        }

    }
}
