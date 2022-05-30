using ColegioMozart.Application.AcademicLevels;
using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Application.Sections;
using ColegioMozart.Application.Shifts;
using ColegioMozart.Application.Teachers;
using ColegioMozart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMozart.Application.ClassRoom.dtos 
{
    public class CreateClassRoomDto : IMapTo<EClassRoom>
    {
        [Display(Name = "Tutor")]
        public Guid TutorId { get; set; }

        [Display(Name = "Turno")]
        public int ShiftId { get; set; }

        [Display(Name = "Grado")]
        public int LevelId { get; set; }

        [Display(Name = "Sección")]
        public int SectionId { get; set; }

        [Display(Name = "Año")]
        public int Year { get; set; }
    }
}
