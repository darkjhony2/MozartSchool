using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.AttendanceStatus.Dtos;

public class AttendanceStatusDTO : IMapFrom<EAttendanceStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Abbreviation { get; set; }
}
