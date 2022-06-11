namespace ColegioMozart.Application.Evaluations.Dtos;

public class AddEvaluationResource
{
    public int EvaluationTypeId { get; set; }
    public string EvaluationName { get; set; }
    public Guid SubjectId { get; set; }
    public Guid AcademicPeriodId { get; set; }
    public Guid ClassRoomId { get; set; }
    //public Guid TeacherId { get; set; }
    public decimal Weight { get; set; }
    public DateTime EvaluationDate { get; set; }
    public decimal MaximumScore { get; set; }
}
