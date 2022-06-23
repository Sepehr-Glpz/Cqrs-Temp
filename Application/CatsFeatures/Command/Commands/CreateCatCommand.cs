using SGSX.CqrsTemp.Domain.Enums;

namespace SGSX.CqrsTemp.Application.CatsFeatures.Command.Commands;
public class CreateCatCommand : ICommand
{
    public CreateCatCommand()
    {
    }

    public string? Name { get; init; }

    public string? Description { get; init; }

    public CatBreed CatBreed { get; init; }
}

