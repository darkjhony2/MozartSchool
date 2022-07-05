using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Application.Students.Dtos;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Evaluations.Dtos;

public class EvaluationDetailDTO
{
    public EvaluationResource Evaluation { get; set; }
    public List<EvaluationScoreDetailDto> Scores { get; set; }
}


public class EvaluationScoreDetailDto : IMapFrom<EEvaluationScore>
{
    public StudentDTO Student { get; set; }
    public decimal Score { get; set; }
    public string? Comments { get; set; }
}