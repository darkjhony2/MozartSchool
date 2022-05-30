﻿using ColegioMozart.Application.Genders.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers
{
    public class GenderController : RestApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await Mediator.Send(new GetAllGendersQuery());
            return Ok(data);
        }

    }
}
