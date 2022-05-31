﻿using ColegioMozart.Application.DocumentTypes.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMozart.Controllers
{
    public class DocumentTypeController : RestApiControllerBase
    {

        /// <summary>
        /// Listar los tipos de documento
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await Mediator.Send(new GetAllDocumentTypesQuery());
            return Ok(data);
        }
    }
}
