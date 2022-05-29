using ColegioMozart.Application.Common.CommonCRUD.Commands;
using ColegioMozart.Application.Common.CommonCRUD.Queries;
using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using WebUI.Pages.Shared;

namespace WebUI.Pages.Common
{
    public class DeleteModel : PageModelBase
    {
        [BindProperty]
        public object EModel { get; set; }

        public EView View { get; set; }

        public string DeleteMessageError { get; set; }

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

        public async Task<IActionResult> OnPostAsync(string? id, string view)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await Mediator.Send(new DeleteEntityCommand() { Id = id, View = view });
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DeleteFailureException ex)
            {
                var response = await Mediator.Send(new GetEntityByIdQuery() { Id = id, View = view });
                EModel = response.Item1;
                View = response.Item2;

                DeleteMessageError = ex.Message;
                return Page();
            }

            return RedirectToPage("./Index", new { view = view });
        }
    }
}
