using ColegioMozart.Application.AttendanceStatus.Dtos;
using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.AttendanceRecord.Dtos;

public class AttendanceRecordDTO : IMapFrom<EAttendanceRecord>
{
    public StudentAttendanceRecordDTO Student { get; set; }
    public DateOnly Date { get; set; }
    public AttendanceStatusDTO AttendanceStatus { get; set; }
    public string? Comments { get; set; }
}

public class StudentAttendanceRecordDTO : IMapFrom<EStudent>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string MothersLastName { get; set; }
    public string LastName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EStudent, StudentAttendanceRecordDTO>()
            .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Person.Name))
            .ForMember(x => x.MothersLastName, opt => opt.MapFrom(s => s.Person.MothersLastName))
            .ForMember(x => x.LastName, opt => opt.MapFrom(s => s.Person.LastName));

    }
}