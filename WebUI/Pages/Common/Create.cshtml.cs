using ColegioMozart.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Common
{
    public class CreateModel : PageModel
    {

        public EView View { get; set; }

        public void OnGet()
        {
        }
    }
}
