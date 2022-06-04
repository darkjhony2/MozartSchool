using FluentValidation;

namespace ColegioMozart.Application.AcademicPeriods.Commands
{
    public class CreateAcademicPeriodCommandValidator : AbstractValidator<CreateAcademicPeriodCommand>
    {
        private static List<DayOfWeek> NotAllowedDays = new List<DayOfWeek> {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday
        };

        public CreateAcademicPeriodCommandValidator()
        {
            RuleFor(x => x.Resource.EndDate.Year).Equal(DateTime.Now.Year);
            RuleFor(x => x.Resource.StartDate.Year).Equal(DateTime.Now.Year);
            RuleFor(x => x.Resource.EndDate).GreaterThan(r => r.Resource.StartDate);
            RuleFor(x => x.Resource.Name).NotNull().NotEmpty();

            RuleFor(x => x.Resource.EndDate.DayOfWeek).Must(x => NotAllowedDays.Contains(x))
          .WithMessage("No se permite registrar como fin de periodo academico un fin de semana.");

            RuleFor(x => x.Resource.StartDate.DayOfWeek).Must(x => NotAllowedDays.Contains(x))
                .WithMessage("No se permite registrar como inicio de periodo academico un fin de semana.");
        }
    }
}
