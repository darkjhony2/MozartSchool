using ColegioMozart.Application.AcademicLevels;
using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Application.Sections;
using ColegioMozart.Application.Shifts;
using ColegioMozart.Application.Teachers;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.StudentClassroom.Queries.StudentsByClassroom
{
    public class StudentClassroomDTO : IMapFrom<EClassRoom>
    {
        public Guid Id { get; set; }
        public TeacherDTO Tutor { get; set; }
        public ShiftDTO Shift { get; set; }
        public AcademicLevelDTO Level { get; set; }
        public SectionDTO Section { get; set; }
        public int Year { get; set; }
        public IList<StudentClassroomListDTO> Students { get; set; }

    }

    public class StudentClassroomListDTO : IMapFrom<EStudent>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MothersLastName { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<EStudent, StudentClassroomListDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Person.Name))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Person.LastName))
                .ForMember(d => d.MothersLastName, opt => opt.MapFrom(s => s.Person.MothersLastName));
        }
    }
}
