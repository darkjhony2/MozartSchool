using ColegioMozart.Application.Common.CommonCRUD.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Pages.Shared.Components.MenuComponent
{
    public class MenuComponent : ViewComponent 
    {
        private ISender _mediator = null!;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await Mediator.Send(new GetEntitiesMenuQuery());

            return View("Default", model);
        }

    }
}
