using Microsoft.Extensions.Logging;
using Infrastructure.Common;
using SGSX.CqrsTemp.Persistence;
using SGSX.CqrsTemp.Application.CatsFeatures.Command;
using SGSX.CqrsTemp.Domain.Results;
using SGSX.CqrsTemp.Application.CatsFeatures.Command.Commands;
using SGSX.CqrsTemp.Domain.Models;

namespace Infrastructure.CatsFeatures;
internal class CatCommandService : BaseCommandService, ICatCommandService
{
    public CatCommandService(ICommandUnitOfWork unitOfWork, ILogger<CatCommandService> logger) : base(unitOfWork, logger)
    {
    }

    public Result<Guid> CreatNewCat(CreateCatCommand createCatCommand)
    {
        var (name, desc, breed) = createCatCommand;
        var newCat = new Cat(null, null, name!, desc!, breed);
        var createResult = UnitOfWork!.CatsRepository.Create(newCat);
        if(createResult.Success == false)
        {
            return createResult.ToResult<Guid>();
        }
        var saveResult = UnitOfWork!.SaveChanges();
        return saveResult.ToResult<Guid>().WithValue(newCat.Id);
    }

    public async Task<Result<Guid>> CreatNewCatAsync(CreateCatCommand createCatCommand)
    {
        var (name, desc, breed) = createCatCommand;
        var newCat = new Cat(null, null, name!, desc!, breed);
        var createResult = UnitOfWork!.CatsRepository.Create(newCat);
        if (createResult.Success == false)
        {
            return createResult.ToResult<Guid>();
        }
        var saveResult = await UnitOfWork!.SaveChangesAsync();
        return saveResult.ToResult<Guid>().WithValue(newCat.Id);
    }
}

