#nullable disable
using Microsoft.AspNetCore.Mvc;
using WebUI.Pages.Shared;
using ColegioMozart.Application.Subjects.Queries.GetSubjectById;
using ColegioMozart.Application.Subjects.Queries;
using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Subjects.Commands.DeleteSubject;

namespace WebUI.Pages.Subjects;

public class DeleteModel : PageModelBase
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

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            await Mediator.Send(new DeleteSubjectCommand() { Id = id.Value });
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return RedirectToPage("./Index");
    }
}
