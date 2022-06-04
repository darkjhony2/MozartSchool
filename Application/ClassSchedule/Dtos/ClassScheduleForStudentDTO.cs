using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Application.Subjects.Queries;
using ColegioMozart.Domain.Entities;
using System.Globalization;

namespace ColegioMozart.Application.ClassSchedule.Dtos;

public class ClassScheduleForStudentDTO : IMapFrom<EClassSchedule>
{
    public TimeOnly EndTime { get; set; }
    public TimeOnly StartTime { get; set; }
    public string DayOfWeek { get; set; }
    public ESubjectDTO Subject { get; set; }
    public TeacherStudentScheduleDTO Teacher { get; set; }

    public void Mapping(Profile profile)
    {
        var culture = new CultureInfo("es-PE");

        profile.CreateMap<EClassSchedule, ClassScheduleForStudentDTO>()
            .ForMember(d => d.DayOfWeek, opt => opt.MapFrom(s => culture.DateTimeFormat.GetDayName(s.DayOfWeek)));
    }
}

public class TeacherStudentScheduleDTO : IMapFrom<ETeacher>
{
    public Guid Id { get; set; }
    public string FullName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ETeacher, TeacherStudentScheduleDTO>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.FullName, opt => opt.MapFrom(s => $"{s.Person.Name} {s.Person.LastName} {s.Person.MothersLastName}"));  
    }
}