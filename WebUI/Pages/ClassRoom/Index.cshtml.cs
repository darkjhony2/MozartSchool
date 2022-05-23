using ColegioMozart.Application.ClassRoom.dtos;
using ColegioMozart.Application.ClassRoom.Queries;
using ColegioMozart.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Pages.Shared;

namespace WebUI.Pages.ClassRoom
{
    public class IndexModel : PageModelBase
    {
        public IList<ClassRoomDto> ClassRooms { get; set; }
        public async Task OnGetAsync()
        {
            ClassRooms = await Mediator.Send(new GetAllClassRoomsByYear()
            {
                Year = DateTime.Now.Year
            });
        }
    }
}
