#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ColegioMozart.Domain.Entities;
using WebUI.Pages.Shared;
using ColegioMozart.Application.Subjects.Queries.GetSubjectById;
using ColegioMozart.Application.Subjects.Queries;
using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Subjects.Commands.UpdateSubject;

namespace WebUI.Pages.Subjects
{
    public class EditModel : PageModelBase
    {

        [BindProperty]
        public ESubjectDTO Model { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                Model = await Mediator.Send(new GetSubjectByIdQuery() { Id = id.Value });
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await Mediator.Send(new UpdateSubjectCommand() { Id = Model.Id, Dto = Model });
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (EntityAlreadyExistException ex)
            {
                foreach (var prop in ex.Fields)
                {
                    ModelState.TryAddModelError("Model." + prop, ex.Message);
                }

                return Page();
            }


            return RedirectToPage("./Index");
        }

    }
}
