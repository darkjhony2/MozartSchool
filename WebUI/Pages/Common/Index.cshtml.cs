#nullable disable
using WebUI.Pages.Shared;
using ColegioMozart.Application.Common.CommonCRUD.Queries;
using ColegioMozart.Domain.Common;

namespace WebUI.Pages.Common
{
    public class IndexModel : PageModelBase 
    {
        public IList<object> EModel { get; set; }

        public EView View { get; set; }

        public async Task OnGetAsync(string view)
        {
            var response = await Mediator.Send(new GetEntitiesQuery()
            {
                View = view
            });

            EModel = response.Models;
            View = response.View;


        }
    }
}
