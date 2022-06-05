using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.EvaluationTypes.Dtos;

public class EvaluationTypeDTO : IMapFrom<EEvaluationType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}
