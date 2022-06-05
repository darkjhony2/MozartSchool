namespace ColegioMozart.Domain.Entities;

public class EEvaluationType : AuditableEntity<int>
{
    public string Name { get; set; }

    public string? Description { get; set; }
}
