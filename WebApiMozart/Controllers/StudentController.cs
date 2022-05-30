﻿using ColegioMozart.Application.Students.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers
{
    public class StudentController : RestApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await Mediator.Send(new GetAllStudentsQuery());
            return Ok(data);
        }

        [HttpGet("academicLevel/{id}")]
        public async Task<IActionResult> GetByCurrentLevel(int id)
        {
            var data = await Mediator.Send(new GetStudentsByAcademicLevelQuery() { AcademicLevelId = id });
            return Ok(data);
        }

    }
}
