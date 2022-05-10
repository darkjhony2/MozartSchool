using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ColegioMozart.Application.AcademicPeriods;

public class CreateAcademicPeriodDTO : IMapTo<EAcademicPeriod>
{
    [Display(Name = "Nombre")]
    [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]+[0-9]*$", ErrorMessage = "Solo se permiten letras y números")]
    [Required]
    public string Name { get; set; }

    [Display(Name = "Fecha de inicio")]
    [Required]
    public DateTime StartDate { get; set; }

    [Display(Name = "Fecha de fin")]
    [Required]
    public DateTime EndDate { get; set; }


    /*[Display(Name = "Año")]
    [Required]
    public int Year { get; set; }*/

    //public 

}
