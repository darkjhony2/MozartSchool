using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Shared;

public abstract class PageModelBase : PageModel
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

}
