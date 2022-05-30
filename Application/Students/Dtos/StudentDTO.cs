using ColegioMozart.Application.AcademicLevels;
using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Students.Dtos;

public class StudentDTO : IMapFrom<EStudent>
{
    public Guid Id { get; set; }
    public string DocumentType { get; set; }
    public string DocumentNumber { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string MothersLastName { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public AcademicLevelDTO CurrentAcademicLevel { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EStudent, StudentDTO>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.DocumentType, opt => opt.MapFrom(s => s.Person.DocumentType.Name))
            .ForMember(d => d.DocumentNumber, opt => opt.MapFrom(s => s.Person.DocumentNumber))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Person.Name))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Person.LastName))
            .ForMember(d => d.MothersLastName, opt => opt.MapFrom(s => s.Person.MothersLastName))
            .ForMember(d => d.Age, opt => opt.MapFrom(s => s.Person.DateOfBirth.GetAge()))
            .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Person.Gender.Name))
            .ForMember(d => d.CurrentAcademicLevel, opt => opt.MapFrom(s => s.CurrentAcademicLevel));
    }
}
