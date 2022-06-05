namespace ColegioMozart.Application.AttendanceRecord.Dtos;

public class RegisterAttendaceRecordResource
{
    public Guid StudentId { get; set; }
    public int AttendanceStatusId { get; set; }
    public string Comments { get; set; }
}
