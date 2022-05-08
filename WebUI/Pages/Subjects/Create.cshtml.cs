#nullable disable
using Microsoft.AspNetCore.Mvc;
using WebUI.Pages.Shared;
using ColegioMozart.Application.Subjects.Commands.CreateSubject;
using ColegioMozart.Application.Common.Exceptions;

namespace WebUI.Pages.Subjects
{
    public class CreateModel : PageModelBase
    {

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateSubjectCommand Model { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await Mediator.Send(Model);
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
