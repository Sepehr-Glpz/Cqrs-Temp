using FluentValidation;
using SGSX.CqrsTemp.Application.CatsFeatures.Command.Commands;
using SGSX.CqrsTemp.Application.Resources;

namespace SGSX.CqrsTemp.Application.CatsFeatures.Command.Validators;
public class CreateCatCommandValidator : AbstractValidator<CreateCatCommand>
{
    public CreateCatCommandValidator()
    {
        RuleFor(c => c.CatBreed)
            .IsInEnum()
            .WithMessage(ErrorMessages.FVFieldInvalid)
            .WithName(DataDictionary.CatBreed);

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(ErrorMessages.FVFieldRequired)
            .Length(3, 30)
            .WithMessage(ErrorMessages.FVFieldLengthInvalid)
            .WithName(DataDictionary.Name);

        RuleFor(c => c.Description)
            .Length(0, 400)
            .WithMessage(ErrorMessages.FVFieldLengthInvalid)
            .When(x => string.IsNullOrWhiteSpace(x.Description) == false);
    }
}

