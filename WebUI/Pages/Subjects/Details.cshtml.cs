#nullable disable
using Microsoft.AspNetCore.Mvc;
using WebUI.Pages.Shared;
using ColegioMozart.Application.Subjects.Queries.GetSubjectById;
using ColegioMozart.Application.Subjects.Queries;
using ColegioMozart.Application.Common.Exceptions;

namespace WebUI.Pages.Subjects;

public class DetailsModel : PageModelBase
{

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
}
