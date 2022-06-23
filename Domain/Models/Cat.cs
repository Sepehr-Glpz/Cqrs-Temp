using SGSX.CqrsTemp.Domain.Base;
using SGSX.CqrsTemp.Domain.Enums;
using SGSX.CqrsTemp.Domain.Results;

namespace SGSX.CqrsTemp.Domain.Models;
public class Cat : BaseEntity
{
    public Cat(Guid? mouseBuddyId, MouseBuddy? mouseBuddy, string name, string description, CatBreed catBreed) : base() =>
        (MouseBuddyId, MouseBuddy, Name, Description, CatBreed) = (mouseBuddyId, mouseBuddy, name, description, catBreed);

    public Cat(Guid id, DateTime createDate, Guid? mouseBuddyId, MouseBuddy? mouseBuddy, string name, string description, CatBreed catBreed) : base(id, createDate) =>
        (MouseBuddyId, MouseBuddy, Name, Description, CatBreed) = (mouseBuddyId, mouseBuddy, name, description, catBreed);

    public Guid? MouseBuddyId { get; internal protected set; }
    public virtual MouseBuddy? MouseBuddy { get; internal protected set; }

    public string Name { get; internal protected set; }

    public string Description { get; internal protected set; }

    public CatBreed CatBreed { get; internal protected set; }

    public Result UpdateCat(UpdateCat updateCat)
    {
        Result finalRes = Result.CreateSuccessful();
        if (updateCat is { CatBreed: not null } breedUpdate)
        {
            var (_, _, breed) = breedUpdate;
            if (breed!.Value == CatBreed.Undefined)
            {
                _ = finalRes.Failed().WithValidation(Validation.FromApplicationError("Breed Cannot be undefined!"));
            }
            else
            {
                this.CatBreed = breed!.Value;
            }
        }
        if (updateCat is { Description: not null } decUpdate)
        {
            var (_, description, _) = decUpdate;
            if (string.IsNullOrWhiteSpace(description))
            {
                _ = finalRes.Failed().WithValidation(Validation.FromApplicationError("Description cannot be empty"));
            }
            else
            {
                this.Description = description;
            }
        }
        if (updateCat is { Name: not null } nameUpdate)
        {
            var (name, _, _) = nameUpdate;
            if (string.IsNullOrWhiteSpace(name))
            {
                _ = finalRes.Failed().WithValidation(Validation.FromApplicationError("Name cannot be empty!"));
            }
            else
            {
                this.Name = name;
            }
        }
        return finalRes;
    }
}

