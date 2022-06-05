namespace ColegioMozart.Domain.Entities;

public class EEvaluation : AuditableEntity<Guid>
{
    public virtual EEvaluationType EvaluationType { get; set; }
    public int EvaluationTypeId { get; set; }

    public string EvaluationName { get; set; }

    public virtual ESubject Subject { get; set; }
    public Guid SubjectId { get; set; }


    public virtual EAcademicPeriod AcademicPeriod { get; set; }
    public Guid AcademicPeriodId { get; set; }

    public virtual EClassRoom ClassRoom { get; set; }
    public Guid ClassRoomId { get; set; }


    public virtual ETeacher Teacher { get; set; }
    public Guid TeacherId { get; set; }

    public decimal Weight { get; set; }

    public DateTime EvaluationDate { get; set; }

    public DateTime MaxEditDate { get; set; } 

    public decimal MaximumScore { get; set; }
}
