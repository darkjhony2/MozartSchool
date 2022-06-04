using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMozart.Application.Subjects.Commands.CreateSubject
{
    public class CreateSubjectValidator : AbstractValidator<CreateSubjectCommand>
    {
        public CreateSubjectValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .Matches(x => "^[A-zÀ-ú]+[ a-zA-Z-_]+[0-9]*$").WithMessage("Solo se permiten letras y números para el nombre");

        }

    }
}
