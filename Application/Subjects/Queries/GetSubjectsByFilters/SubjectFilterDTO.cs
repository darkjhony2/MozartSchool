using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Subjects.Queries.GetSubjectsByFilters
{
    public class SubjectFilterDTO : IMapTo<ESubject>
    {
        public string Name { get; set; }
    }
}
