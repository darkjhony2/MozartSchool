using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ColegioMozart.Application.Sections;

public class SectionDTO : IMapFrom<ESection>
{
    [Display(Name = "Id")]
    public int Id { get; set; }

    [Display(Name = "Sección")]
    public string Name { get; set; }

}
