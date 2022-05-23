using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMozart.Domain.Entities
{
    public class EClassRoom : AuditableEntity<Guid>
    {
        public int Year {  get; set; }
        public virtual EAcademicLevel Level { get; set; }
        public int LevelId { get; set; }
        public virtual EShift Shift { get; set; }
        public int ShiftId { get; set; }
        public virtual ETeacher Tutor { get; set; }
        public Guid TutorId { get; set; }
        public virtual ESection Section { get; set; }
        public int SectionId { get; set; }
    }
}
