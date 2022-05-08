using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ColegioMozart.Application.Subjects.Queries;

public class ESubjectDTO : IMapFrom<ESubject>
{
    [Display(Name = "Identificador")]
    public Guid Id { get; set; }

    [Display(Name = "Nombre")]
    public string Name { get; set; }

}
