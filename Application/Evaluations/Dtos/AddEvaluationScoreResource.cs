namespace ColegioMozart.Application.Evaluations.Dtos
{
    public class AddEvaluationScoreResource
    {
        public Guid StudentId { get; set; }

        public decimal Score { get; set; }

        public string? Comments { get; set; }
    }
}
