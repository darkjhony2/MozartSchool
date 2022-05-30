using FluentValidation;

namespace ColegioMozart.Application.AcademicPeriods.Commands
{
    public class CreateAcademicPeriodCommandValidator : AbstractValidator<CreateAcademicPeriodCommand>
    {
        public CreateAcademicPeriodCommandValidator()
        {
            RuleFor(x => x.Resource.EndDate.Year).Equal(DateTime.Now.Year);
            RuleFor(x => x.Resource.StartDate.Year).Equal(DateTime.Now.Year);
            RuleFor(x => x.Resource.EndDate).GreaterThan(r => r.Resource.StartDate);
        }
    }
}
