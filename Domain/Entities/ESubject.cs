namespace ColegioMozart.Domain.Entities;

public class ESubject : AuditableEntity<Guid>
{
    public string Name { get; set; }
}
