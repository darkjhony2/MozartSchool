using ColegioMozart.Application.AcademicScale.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers
{
    public class AcademicScaleController : RestApiControllerBase
    {
        /// <summary>
        /// Listar todas las escalas académicas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await Mediator.Send(new GetAcademicScalesQuery());
            return Ok(data);
        }

        

    }
}
