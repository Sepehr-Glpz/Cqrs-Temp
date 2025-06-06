﻿using SGSX.CqrsTemp.Application.Common.Command;
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

    public void Deconstruct(out string? name, out string? description, out CatBreed catBreed) =>
        (name, description, catBreed) = (Name, Description, CatBreed);
}

