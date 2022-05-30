namespace ColegioMozart.Domain.Entities
{
    public class EClassSchedule : AuditableEntity<Guid>
    {
        public TimeOnly EndTime { get; set; }
        public TimeOnly StartTime { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public virtual ESubject Subject { get; set; }
        public Guid SubjectId { get; set; }
        public virtual ETeacher Teacher { get; set; }
        public Guid TeacherId { get; set; }
        public virtual EClassRoom ClassRoom { get; set; }
        public Guid ClassRoomId { get; set; }
    }
}
