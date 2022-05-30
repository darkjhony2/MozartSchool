using ColegioMozart.Application.AcademicPeriods;
using ColegioMozart.Application.AcademicPeriods.Commands;
using ColegioMozart.Application.AcademicPeriods.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers;

public class AcademicPeriodController : RestApiControllerBase
{

    /// <summary>
    /// Obtiene una lista de los periodos académicos del presente año
    /// </summary>
    /// <returns></returns>
    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        var data = await Mediator.Send(new GetAcademicPeriodCurrentYearQuery());
        return Ok(data);
    }


    /// <summary>
    /// Registra un periodo academico para el año actual
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost()]
    public async Task<IActionResult> Create([FromBody] CreateAcademicPeriodDTO dto)
    {
        await Mediator.Send(new CreateAcademicPeriodCommand() { Resource = dto });
        return Ok(dto);
    }
}
