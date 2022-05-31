using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.AcademicScale.Dtos;

public class AcademicScaleDTO : IMapFrom<EAcademicScale>
{
    public int Id { get; set; }
    public string Name { get; set; }
}
