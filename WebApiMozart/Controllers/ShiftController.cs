using ColegioMozart.Application.Shifts;
using ColegioMozart.Application.Shifts.Commands;
using ColegioMozart.Application.Shifts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers;

public class ShiftController : RestApiControllerBase
{

    /// <summary>
    /// Listar todos los turnos 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var subjects = await Mediator.Send(new GetShiftsQuery());
        return Ok(subjects);
    }

    /// <summary>
    /// Buscar un turno por su Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var subject = await Mediator.Send(new GetShiftByIdQuery() { Id = id });
        return Ok(subject);
    }

    /// <summary>
    /// Crear un turno
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateShiftCommand cmd)
    {
        await Mediator.Send(cmd);
        return Ok(cmd);
    }

    /// <summary>
    /// Actualiza un turno
    /// </summary>
    /// <param name="id"></param>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateShiftResource resource)
    {
        await Mediator.Send(new UpdateShiftCommand() { Id = id, Resource = resource });
        return Ok(resource);
    }


    /// <summary>
    /// Elimina un turno
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteShiftCommand() { Id = id });
        return Ok();
    }

}
