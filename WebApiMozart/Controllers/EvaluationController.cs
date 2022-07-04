using ColegioMozart.Application.Evaluations.Commands;
using ColegioMozart.Application.Evaluations.Dtos;
using ColegioMozart.Application.Evaluations.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers;

public class EvaluationController : RestApiControllerBase
{

    /// <summary>
    /// Obtiene el listado de evaluaciones de un docente
    /// </summary>
    /// <returns></returns>
    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        var response = await Mediator.Send(new GetEvaluationByTeacherQuery() { Id = null });
        return Ok(response);
    }

    /// <summary>
    /// Registra una evaluacion para el salon
    /// </summary>
    /// <returns></returns>
    [HttpPost()]
    public async Task<IActionResult> Create([FromBody] AddEvaluationResource dto)
    {
        await Mediator.Send(new AddEvaluationToClassroomCommand() { Resource = dto });
        return Ok(dto);
    }


    /// <summary>
    /// Registra las notas para una evaluacion
    /// </summary>
    /// <returns></returns>
    [HttpPost("{EvaluationId}/Scores")]
    public async Task<IActionResult> RegisterScore([FromRoute] Guid EvaluationId, [FromBody] List<AddEvaluationScoreResource> dto)
    {
        await Mediator.Send(new AddEvaluationScoreCommand() { Scores = dto, EvaluationId = EvaluationId });
        return Ok(dto);
    }


}
