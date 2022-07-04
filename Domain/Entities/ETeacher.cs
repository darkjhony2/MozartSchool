namespace ColegioMozart.Domain.Entities;

public class ETeacher : AuditableEntity<Guid>
{
    public virtual EPerson Person { get; set; }
    public Guid PersonId { get; set; }

    public string Email { get; set; }
    public string Phone { get; set; }
}
