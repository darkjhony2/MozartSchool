using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Teachers.Dtos;

public class UpdateTeacherResource : IMapTo<ETeacher>
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string MothersLastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int DocumentTypeId { get; set; }
    public string DocumentNumber { get; set; }
    public int GenderId { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateTeacherResource, ETeacher>()
            .ForPath(x => x.Person.Name, opt => opt.MapFrom(s => s.Name))
            .ForPath(x => x.Person.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForPath(x => x.Person.MothersLastName, opt => opt.MapFrom(s => s.MothersLastName))
            .ForPath(x => x.Person.DateOfBirth, opt => opt.MapFrom(s => s.DateOfBirth))
            .ForPath(x => x.Person.DocumentTypeId, opt => opt.MapFrom(s => s.DocumentTypeId))
            .ForPath(x => x.Person.DocumentNumber, opt => opt.MapFrom(s => s.DocumentNumber))
            .ForPath(x => x.Person.GenderId, opt => opt.MapFrom(s => s.GenderId));
    }
}
