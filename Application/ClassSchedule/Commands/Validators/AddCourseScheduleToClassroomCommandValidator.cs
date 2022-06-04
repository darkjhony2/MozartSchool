using ColegioMozart.Domain.Business;
using FluentValidation;

namespace ColegioMozart.Application.ClassSchedule.Commands.Validators;

public class AddCourseScheduleToClassroomCommandValidator : AbstractValidator<AddCourseScheduleToClassroomCommand>
{
    public AddCourseScheduleToClassroomCommandValidator()
    {
        RuleFor(x => x.EndTime).NotNull();
        RuleFor(x => x.StartTime).NotNull().LessThan(r => r.EndTime)
            .WithMessage("La hora de incio tiene que ser menor a la hora de fin.");

        RuleFor(x => x.DayOfWeek).Must(x => Constants.AllowedDays.Contains(x))
           .WithMessage("Día inválido");
    }
}
