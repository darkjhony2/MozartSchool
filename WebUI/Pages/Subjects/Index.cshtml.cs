#nullable disable
using WebUI.Pages.Shared;
using ColegioMozart.Application.Subjects.Queries;

namespace WebUI.Pages.Subjects
{
    public class IndexModel : PageModelBase
    {
        public IList<ESubjectDTO> ESubject { get;set; }

        public async Task OnGetAsync()
        {
            ESubject = await Mediator.Send(new GetSubjectsQuery());
        }
    }
}
