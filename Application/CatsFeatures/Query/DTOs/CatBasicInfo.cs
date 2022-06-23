
using SGSX.CqrsTemp.Domain.Enums;

namespace SGSX.CqrsTemp.Application.CatsFeatures.Query.DTOs;
public class CatBasicInfo
{
    public CatBasicInfo()
    {
    }

    public CatBasicInfo(Guid id, DateTime createDate, string? name, string? description, CatBreed catBreed) =>
        (Id, CreateDate, Name, CatBreed, Description) = (id, createDate, name, catBreed, description);

    public Guid Id { get; init; }
    public DateTime CreateDate { get; init; }
    public string? Name { get; init; }
    public CatBreed CatBreed { get; init; }
    public string? Description { get; init; }
}

