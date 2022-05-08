namespace ColegioMozart.Domain.Entities;

public class EAcademicPeriod : AuditableEntity<Guid>
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public int Year { get; set; }

    public string Name { get; set; }
}
