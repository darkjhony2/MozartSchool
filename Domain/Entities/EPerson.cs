namespace ColegioMozart.Domain.Entities;

public class EPerson : AuditableEntity<Guid>
{   
    public string Name { get; set; }
    public string MothersLastName { get; set; }
    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public virtual EDocumentType DocumentType { get; set; }

    public int DocumentTypeId { get; set; }

    public string DocumentNumber { get; set; }

    public virtual EGender Gender { get; set; }

    public int GenderId { get; set; }
}
