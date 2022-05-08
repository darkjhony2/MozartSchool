namespace ColegioMozart.Domain.Entities;

public class EAcademicLevel : AuditableEntity<int>
{

    public string Level { get; set; }

    public int? PreviousAcademicLevelId { get; set; }

    public virtual EAcademicLevel PreviousAcademicLevel { get; set; }

}
