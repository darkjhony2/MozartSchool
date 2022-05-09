using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ColegioMozart.Application.Shifts;

public class CreateShiftDTO : IMapTo<EShift>
{
    [Display(Name = "Nombre")]
    [Required]
    public string Name { get; set; }
}
