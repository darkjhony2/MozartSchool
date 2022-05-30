using ColegioMozart.Application.Shifts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers;

public class ShiftController : RestApiControllerBase
{

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var subjects = await Mediator.Send(new GetShiftsQuery());
        return Ok(subjects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var subject = await Mediator.Send(new GetShiftByIdQuery() { Id = id });
        return Ok(subject);
    }
}
