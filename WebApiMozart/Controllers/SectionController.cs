using ColegioMozart.Application.Sections.Commands;
using ColegioMozart.Application.Sections.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers
{
    public class SectionController : RestApiControllerBase
    {
        /// <summary>
        /// Obtiene un listado de las secciones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await Mediator.Send(new GetSectionsQuery());
            return Ok(data);
        }

        /// <summary>
        /// Busca una sección por su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await Mediator.Send(new GetSectionByIdQuery() { Id = id });
            return Ok(data);
        }


        /// <summary>
        /// Registra una nueva sección
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateSectionCommand cmd)
        {
            await Mediator.Send(cmd);
            return Ok(cmd);
        }

        /// <summary>
        /// Elimina una sección por su ID
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await Mediator.Send(new DeleteSectionCommand() { Id = id });
            return Ok();
        }

    }
}
