using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ColegioMozart.Application.Sections;

public class CreateSectionDTO : IMapTo<ESection>
{

    [Display(Name = "Nombre")]
    [Required]
    [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]+[0-9]*$", ErrorMessage = "Solo se permiten letras y números")]
    public string Name { get; set; }
}
