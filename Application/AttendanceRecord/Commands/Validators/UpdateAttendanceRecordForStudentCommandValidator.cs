using ColegioMozart.Domain.Business;
using FluentValidation;

namespace ColegioMozart.Application.AttendanceRecord.Commands.Validators;

public class UpdateAttendanceRecordForStudentCommandValidator : AbstractValidator<UpdateAttendanceRecordForStudentCommand>
{
    public UpdateAttendanceRecordForStudentCommandValidator()
    {
        RuleFor(x => x.Date).NotNull().Must(x => Constants.AllowedDays.Contains(x.Date.DayOfWeek))
            .WithMessage("No se permite tomar asistencia en un día que es fin de semana.");

        RuleFor(x => x.Resource).NotNull();
    }
}
