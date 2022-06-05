namespace ColegioMozart.Domain.Entities;

public class EAttendanceStatus : AuditableEntity<int>
{
    public string Name { get; set; }
    public string Abbreviation { get; set; }
}
