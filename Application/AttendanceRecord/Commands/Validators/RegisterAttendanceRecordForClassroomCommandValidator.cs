using ColegioMozart.Application.AttendanceRecord.Dtos;
using ColegioMozart.Domain.Business;
using FluentValidation;

namespace ColegioMozart.Application.AttendanceRecord.Commands.Validators;

public class RegisterAttendanceRecordForClassroomCommandValidator : AbstractValidator<RegisterAttendanceRecordForClassroomCommand>
{

    public RegisterAttendanceRecordForClassroomCommandValidator()
    {
        RuleFor(x => x.ClassroomId).NotNull();
        RuleFor(x => x.Date).NotNull().Must(x => Constants.AllowedDays.Contains(x.Date.DayOfWeek))
            .WithMessage("No se permite tomar asistencia en un día que es fin de semana.");

        RuleFor(x => x).Must(x => x.StudentsAttendance.Any())
            .WithMessage("Debe tener al menos un alumno para tomar la asistencia")
            .Must(x => IsDuplicate(x.StudentsAttendance))
            .WithMessage("No se permiten alumnos repetidos al registrar la asistencia.");
    }

    private bool IsDuplicate(IEnumerable<RegisterAttendaceRecordResource> list)
    {
        if (list == null)
            return true;

        var students = list.Select(x => x.StudentId);

        bool isUnique = students.Distinct().Count() == students.Count();

        return isUnique;
    }

}
