using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ColegioMozart.Application.AcademicPeriods;

public class AcademicPeriodDTO : IMapFrom<EAcademicPeriod>
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }

    [Display(Name = "Nombre del periodo")]
    public string Name { get; set; }

    [Display(Name = "Fecha de inicio")]
    public DateOnly StartDate { get; set; }

    [Display(Name = "Fecha de fin")]
    public DateOnly EndDate { get; set; }

    
}
