using ColegioMozart.Application.EvaluationTypes.Commands;
using ColegioMozart.Application.EvaluationTypes.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers;

public class EvaluationTypeController : RestApiControllerBase
{


    /// <summary>
    /// Listar todos los tipos de evaluaciones
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await Mediator.Send(new GetAllEvaluationTypesQuery());
        return Ok(data);
    }

    /// <summary>
    /// Registra un nuevo tipo de evaluación
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddEvaluationTypeCommand cmd)
    {
        var data = await Mediator.Send(cmd);
        return Ok(data);
    }


    /// <summary>
    /// Elimina un tipo de evaluación
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var data = await Mediator.Send(new DeleteEvaluationTypeCommand { Id = id });
        return Ok(data);
    }
}
