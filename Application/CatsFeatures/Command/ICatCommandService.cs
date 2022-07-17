using SGSX.CqrsTemp.Application.CatsFeatures.Command.Commands;
using SGSX.CqrsTemp.Domain.Models;
using SGSX.CqrsTemp.Domain.Results;

namespace SGSX.CqrsTemp.Application.CatsFeatures.Command;
public interface ICatCommandService
{
    Result<Guid> CreatNewCat(CreateCatCommand createCatCommand);
    Task<Result<Guid>> CreatNewCatAsync(CreateCatCommand createCatCommand);
}

