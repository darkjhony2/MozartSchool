namespace ColegioMozart.Domain.Entities;

public class EStudent : AuditableEntity<Guid>
{
    public virtual EPerson Person { get; set; }
    public Guid PersonId { get; set; }

    public virtual EClassRoom? ClassRoom { get; set; }

    public Guid? ClassRoomId { get; set; }

    public EAcademicLevel CurrentAcademicLevel { get; set; }
    public int CurrentAcademicLevelId { get; set; }
}
