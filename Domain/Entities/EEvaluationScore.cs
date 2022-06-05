namespace ColegioMozart.Domain.Entities;

public class EEvaluationScore : AuditableEntity<Guid>
{
    public virtual EEvaluation Evaluation { get; set; }
    public Guid EvaluationId { get; set; }


    public virtual EStudent Student { get; set; }
    public Guid StudentId { get; set; }

    public decimal Score { get; set; }

    public string? Comments { get; set; }

}
