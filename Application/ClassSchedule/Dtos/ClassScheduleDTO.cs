using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Application.Subjects.Queries;
using ColegioMozart.Domain.Entities;
using System.Globalization;

namespace ColegioMozart.Application.ClassSchedule.Dtos;

public class ClassScheduleDTO : IMapFrom<EClassSchedule>
{
    public Guid Id { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public string DayOfWeek { get; set; }

    public ESubjectDTO Subject { get; set; }
    public TeacherClassRoomDTO Teacher { get; set; }

    public void Mapping(Profile profile)
    {
        var culture = new CultureInfo("es-PE");
        
        profile.CreateMap<EClassSchedule, ClassScheduleDTO>()
            .ForMember(d => d.DayOfWeek, opt => opt.MapFrom(s => culture.DateTimeFormat.GetDayName(s.DayOfWeek)));
    }
}


public class TeacherClassRoomDTO : IMapFrom<ETeacher>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string MothersLastName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ETeacher, TeacherClassRoomDTO>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Person.Name))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Person.LastName))
            .ForMember(d => d.MothersLastName, opt => opt.MapFrom(s => s.Person.MothersLastName));
    }
}