using ColegioMozart.Application.AcademicLevels;
using ColegioMozart.Application.ClassRoom.dtos;
using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Application.DocumentTypes.Dtos;
using ColegioMozart.Application.Genders.Dtos;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Students.Dtos;

public class StudentDetailDTO : IMapFrom<EStudent>
{
    public StudenPersonDTO Person { get; set; }
    public ClassRoomDto ClassRoom { get; set; }
    public AcademicLevelDTO CurrentAcademicLevel { get; set; }
}


public class StudenPersonDTO : IMapFrom<EPerson>
{
    public string Name { get; set; }
    public string MothersLastName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DocumentTypeDTO DocumentType { get; set; }
    public string DocumentNumber { get; set; }
    public GenderDTO Gender { get; set; }
}