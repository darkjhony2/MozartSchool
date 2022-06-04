using ColegioMozart.Application.AcademicLevels;
using ColegioMozart.Application.ClassRoom.dtos;
using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Application.Sections;
using ColegioMozart.Application.Shifts;
using ColegioMozart.Application.Subjects.Queries;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.ClassSchedule.Dtos;

public class ClassScheduleForTeacherDTO : IMapFrom<EClassSchedule>
{
    public TimeOnly EndTime { get; set; }
    public TimeOnly StartTime { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public ESubjectDTO Subject { get; set; }
    public ClassRoomForClassScheduleDTO ClassRoom { get; set; }
}


public class ClassRoomForClassScheduleDTO : IMapFrom<EClassRoom>
{
    public Guid Id { get; set; }
    public ShiftDTO Shift { get; set; }
    public AcademicLevelDTO Level { get; set; }
    public SectionDTO Section { get; set; }
}