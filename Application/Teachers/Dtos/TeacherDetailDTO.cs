using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Application.DocumentTypes.Dtos;
using ColegioMozart.Application.Genders.Dtos;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Teachers.Dtos;

public class TeacherDetailDTO : IMapFrom<ETeacher>
{
    public TeacherPersonDTO Person { get; set; }

    public string Email { get; set; }
    public string Phone { get; set; }
}


public class TeacherPersonDTO : IMapFrom<EPerson>
{
    public string Name { get; set; }
    public string MothersLastName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DocumentTypeDTO DocumentType { get; set; }
    public string DocumentNumber { get; set; }
    public GenderDTO Gender { get; set; }
}