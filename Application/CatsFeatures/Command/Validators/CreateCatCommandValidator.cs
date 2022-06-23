using FluentValidation;
using SGSX.CqrsTemp.Application.CatsFeatures.Command.Commands;

namespace SGSX.CqrsTemp.Application.CatsFeatures.Command.Validators;
public class CreateCatCommandValidator : AbstractValidator<CreateCatCommand>
{
    public CreateCatCommandValidator()
    {
        
    }
}

