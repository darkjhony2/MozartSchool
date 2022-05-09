using ColegioMozart.Application.Common.CommonCRUD.Commands;
using ColegioMozart.Application.Common.CommonCRUD.Queries;
using ColegioMozart.Application.Shifts;
using ColegioMozart.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Pages.Shared;
using WebUI.Utils;

namespace WebUI.Pages.Common
{
    public class CreateModel : PageModelBase
    {
        public EView View { get; set; }

        [BindProperty]
        public string ViewName { get; set; }

        [BindProperty]
        public dynamic EModel { get; set; }

        [BindProperty]
        public Type ModelType { get; set; }

        public async Task<IActionResult> OnGetAsync(string view)
        {
            View = await Mediator.Send(new GetViewByNameQuery() { View = view });
            ViewName = view;

            if (!string.IsNullOrEmpty(View.Entity.CreateEntityFullName))
            {
                ModelType = ReflectionExtensions.GetTypeFromAllAssemblies(View.Entity.CreateEntityFullName);
            }


            EModel = Activator.CreateInstance(ModelType);

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            View = await Mediator.Send(new GetViewByNameQuery() { View = ViewName });
            if (!string.IsNullOrEmpty(View.Entity.CreateEntityFullName))
            {
                ModelType = ReflectionExtensions.GetTypeFromAllAssemblies(View.Entity.CreateEntityFullName);
            }

            var requestData = Request.Form.Where(x=> x.Key.StartsWith("EModel")).ToDictionary(x => x.Key.Replace("EModel.", string.Empty), x => x.Value.ToString());

            

            if (!ModelState.IsValid)
            {
                return Page();
            }


            await Mediator.Send(new CreateEntityCommand() { Parameters = requestData, View = View });


            return RedirectToPage("./Index", new { view = ViewName });
        }
    }
}
