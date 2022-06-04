using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Students.Dtos;

public class UpdateStudentResource : IMapTo<EPerson>
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string MothersLastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int DocumentTypeId { get; set; }
    public string DocumentNumber { get; set; }
    public int GenderId { get; set; }
}
