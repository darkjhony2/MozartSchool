using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ColegioMozart.Application.Shifts;

public class ShiftDTO : IMapFrom<EShift>
{
    [Display(Name = "Id")]
    public int Id { get; set; }

    [Display(Name = "Turno")]
    public string Name { get; set; }
}
