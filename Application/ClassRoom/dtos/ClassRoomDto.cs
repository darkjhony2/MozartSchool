using ColegioMozart.Application.AcademicLevels;
using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Application.Sections;
using ColegioMozart.Application.Shifts;
using ColegioMozart.Application.Teachers;
using ColegioMozart.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ColegioMozart.Application.ClassRoom.dtos
{
    public class ClassRoomDto : IMapFrom<EClassRoom>
    {
        [Display(Name = "Código")]
        public Guid Id { get; set; }

        [Display(Name = "Tutor")]
        public TeacherDTO Tutor { get; set; }

        [Display(Name = "Turno")]
        public ShiftDTO Shift { get; set; }

        [Display(Name = "Grado")]
        public AcademicLevelDTO Level { get; set; }

        [Display(Name = "Sección")]
        public SectionDTO Section { get; set; }

        [Display(Name = "Año")]
        public int Year { get; set; }
    }
}
