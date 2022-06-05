namespace ColegioMozart.Domain.Entities;

public class EAttendanceRecord : AuditableEntity<Guid>
{
    public virtual EStudent Student { get; set; }
    public Guid StudentId { get; set; }
    
    public DateOnly Date { get; set; }
    
    public virtual EAttendanceStatus AttendanceStatus { get; set; }
    public int AttendanceStatusId { get; set; }

    public virtual EAcademicPeriod AcademicPeriod { get; set; }
    public Guid AcademicPeriodId { get; set; }

    public string? Comments { get; set; }
}
