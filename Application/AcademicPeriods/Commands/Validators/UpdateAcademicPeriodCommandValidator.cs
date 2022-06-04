using ColegioMozart.Domain.Business;
using FluentValidation;

namespace ColegioMozart.Application.AcademicPeriods.Commands.Validators;

public class UpdateAcademicPeriodCommandValidator : AbstractValidator<UpdateAcademicPeriodCommand>
{

    public UpdateAcademicPeriodCommandValidator()
    {
        RuleFor(x => x.Resource.EndDate.Year).Equal(DateTime.Now.Year);
        RuleFor(x => x.Resource.StartDate.Year).Equal(DateTime.Now.Year);
        RuleFor(x => x.Resource.EndDate).GreaterThan(r => r.Resource.StartDate);
        RuleFor(x => x.Resource.Name).NotNull().NotEmpty();

        RuleFor(x => x.Resource.EndDate.DayOfWeek).Must(x => Constants.AllowedDays.Contains(x))
            .WithMessage("No se permite registrar como fin de periodo academico un fin de semana.");

        RuleFor(x => x.Resource.StartDate.DayOfWeek).Must(x => Constants.AllowedDays.Contains(x))
            .WithMessage("No se permite registrar como inicio de periodo academico un fin de semana.");
    }
}
