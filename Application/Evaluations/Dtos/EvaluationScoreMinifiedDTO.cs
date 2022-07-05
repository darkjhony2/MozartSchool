using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Application.Students.Dtos;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Evaluations.Dtos;

public class EvaluationScoreMinifiedDTO : IMapFrom<EEvaluationScore>
{

    // public EvaluationResource Evaluation { get; set; }

    public StudentDTO Student { get; set; }


    public decimal Score { get; set; }

    public string? Comments { get; set; }

}
