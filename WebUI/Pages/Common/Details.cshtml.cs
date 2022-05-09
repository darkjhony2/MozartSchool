using ColegioMozart.Application.Common.CommonCRUD.Queries;
using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using WebUI.Pages.Shared;

namespace WebUI.Pages.Common
{
    public class DetailsModel : PageModelBase
    {
        public object EModel { get; set; }

        public EView View { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id, string view)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var response = await Mediator.Send(new GetEntityByIdQuery() { Id = id, View = view });
                EModel = response.Item1;

                View = response.Item2;
            }
            catch (NotFoundException)
            {
                return NotFound();
            }


            return Page();
        }
    }
}
