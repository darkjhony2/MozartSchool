using ColegioMozart.Application.Common.CommonCRUD.Queries;
using ColegioMozart.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Pages.Shared;

namespace WebUI.Pages.Common
{
    public class CreateModel : PageModelBase
    {
        public EView View { get; set; }

        public async Task<IActionResult> OnGetAsync(string view)
        {
            View = await Mediator.Send(new GetViewByNameQuery() { View = view });
            return Page();
        }
    }
}
