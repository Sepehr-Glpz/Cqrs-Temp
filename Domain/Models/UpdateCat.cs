using SGSX.CqrsTemp.Domain.Enums;

namespace SGSX.CqrsTemp.Domain.Models;
public readonly struct UpdateCat
{
    public readonly string? Name { get; init; }
    public readonly string? Description { get; init; }
    public readonly CatBreed? CatBreed { get; init; }
}

