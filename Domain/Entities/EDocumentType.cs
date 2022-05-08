namespace ColegioMozart.Domain.Entities;

public class EDocumentType : AuditableEntity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string RegexValidation { get; set; }
}
